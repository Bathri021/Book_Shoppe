using Book_Shoppe.App_Start;
using Book_Shoppe.BL;
using Book_Shoppe.Entity;
using Book_Shoppe.Models;
using Book_Shoppe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using System.Web.Security;

namespace Book_Shoppe.Controllers
{
  //  [CustomeErrorHandler()]
    public class UserController : Controller
    {
        public static User CurrentUser = null;
        IUserBL IUserBL = new UserBL();
        // Admin Page for Manage The Users
        // GET: User
        //[AdminAuthorizationFilter]
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            IEnumerable<User> users = IUserBL.GetUsers();
            IUserBL.GetRoles();
            return View(users);
        }

        // Get Meathod for the User Registration Page
        [HandleError]
        public ActionResult Register()
        {
            UserBL userBL = new UserBL();
            ViewBag.Roles = new SelectList(userBL.GetRoles(), "RoleID", "RoleName");
            return View();
        }

        // Post Meathod For the User Registration Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationFormViewModel user)
        {
            ViewBag.Roles = new SelectList(IUserBL.GetRoles(), "RoleID", "RoleName");
           
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }
            else
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegistrationFormViewModel, User>(); });
                IMapper iMapper = config.CreateMapper();
                User _user = iMapper.Map<RegistrationFormViewModel, User>(user);

                ViewBag.Alert = IUserBL.AddUser(_user);

                if (ViewBag.Alert == null)
                {
                    ViewBag.Message = "Registration Successfull";
                    ViewBag.Alert = null;
                }
            }
            
            return View(user);
        }

        // Get Meathod for User Login 
        public ActionResult LogIn()
        {
            return View();
        }

        // Post Meathod for User Login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInFormViewModel user)
        {
            if (ModelState.IsValid)
            {
                User _user = IUserBL.LogIn(user.UserName, user.Password);

                if (_user != null)
                {
                    FormsAuthentication.SetAuthCookie(_user.Name, false);
                    var authTicket = new FormsAuthenticationTicket(1, _user.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false,_user.Role.RoleName);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    CurrentUser = _user;
                    Session["UserID"] = CurrentUser.UserID.ToString();
                    Session["Name"] = CurrentUser.Name.ToString();
                    ViewBag.Message = "Login Successfull";

                    int userId = Convert.ToInt32(Session["UserID"]);
                    return RedirectToAction("UserProfile", new { id = userId });
                }
                else
                {
                    ViewBag.Alert = "Login Failed";
                    return RedirectToAction("LogIn");
                }
            }
                    return RedirectToAction("LogIn");
        }


        // UserProfile Page
        public ActionResult UserProfile(int id)
        {
            User user = IUserBL.GetUserByID(id);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UpdateUserVM>(); });
            IMapper iMapper = config.CreateMapper();
            UpdateUserVM userModel = iMapper.Map<User, UpdateUserVM>(user);
            ViewBag.WishList= IUserBL.GetUserWishlist(id);
            ViewBag.UserCartDetails = IUserBL.GetUserCartDetails(id);
            return View(userModel);
        }

        // Get Meathod for Edit User Profile 
        public ActionResult EditProfile(int id)
        {
            User user = IUserBL.GetUserByID(id);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User,UpdateUserVM>(); });
            IMapper iMapper = config.CreateMapper();
            UpdateUserVM userModel = iMapper.Map<User, UpdateUserVM>(user);
            return View(userModel);
        }

        // Post Meathod for Update Edited User Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateUserVM userModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateUserVM,User>(); });
                IMapper iMapper = config.CreateMapper();
                User user = iMapper.Map<UpdateUserVM,User>(userModel);
                ViewBag.Alert= IUserBL.EditUser(user);
                if (ViewBag.Alert==null)
                {
                    ViewBag.Message = "Update Successfull";
                    ViewBag.Alert = null;
                }
            }
           
            return RedirectToAction("LogOut");
        }

        // Log out Meathod
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            UserController.CurrentUser = null;
            return RedirectToAction("LogIn");
        }

        // View Particular User Details
        public ActionResult ViewDetails(int id)
        {
            User user = IUserBL.GetUserByID(id);
            return View(user);
        }

        // Delete the User
       // [AdminAuthorizationFilter]
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int id)
        {
            IUserBL.Delete(id);
            return RedirectToAction("Index");
        }

        // Add the Book Into the Users WishList
        public ActionResult AddToWishList(int id)
        {
            ViewBag.WishList_Message = null;
            if (CurrentUser==null)
            {
                return RedirectToAction("LogIn");
            }
            int userID = CurrentUser.UserID;
            ViewBag.WishList_Message = IUserBL.AddToWishList(userID, id);

            if (ViewBag.WishList_Message == null)
                ViewBag.WishList_Message = "Book added into the wishlist";
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Remove the Book From The Users WishList
        public ActionResult RemoveBookFormWishlist(int id)
        {
            IUserBL.RemoveBookFormWishlist(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Get The Particular Book Details From BookList
        public JsonResult GetBookDetails(int BookID)
        {
            IBookBL IBookBL = new BookBL();
            Book book = IBookBL.GetBookDetails(BookID);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        // Add The Book Into the Users Orders
        public ActionResult AddToCart(int id)
        {
            ViewBag.OrderList_Message = null;
            UserBL userBL = new UserBL();

            if (CurrentUser == null)
            {
                return RedirectToAction("LogIn");
            }
            int userID = CurrentUser.UserID;
            ViewBag.OrderList_Message = IUserBL.AddToCart(userID,id);

            if (ViewBag.OrderList_Message == null)
                ViewBag.OrderList_Message = "Book added into the Order List";
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Get the Book Details From Book List
        public JsonResult GetOrderedBookDetails(int BookID)
        {
            IBookBL IBookBL = new BookBL();
            Book book = IBookBL.GetBookDetails(BookID);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}