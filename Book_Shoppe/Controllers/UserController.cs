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

namespace Book_Shoppe.Controllers
{
  //  [CustomeErrorHandler()]
    public class UserController : Controller
    {
        public static User CurrentUser = null;
        
        // Admin Page for Manage The Users
        // GET: User
        [AdminAuthorizationFilter]
        public ActionResult Index()
        {
            UserBL userBL = new UserBL();
            IEnumerable<User> users = userBL.GetUsers();
            userBL.GetRoles();
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
            UserBL userBL = new UserBL();
            ViewBag.Roles = new SelectList(userBL.GetRoles(), "RoleID", "RoleName");
           
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }
            else
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegistrationFormViewModel, User>(); });
                IMapper iMapper = config.CreateMapper();
                User _user = iMapper.Map<RegistrationFormViewModel, User>(user);

                ViewBag.Alert = userBL.AddUser(_user);

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
            UserBL userBL = new UserBL();
            if (ModelState.IsValid)
            {
                User _user = userBL.LogIn(user.UserName, user.Password);

                if (_user != null)
                {
                    UserController.CurrentUser = _user;
                    userBL.SetCurrentUser(CurrentUser);
                    Session["UserID"] = CurrentUser.UserID.ToString();
                    Session["RoleID"] = CurrentUser.RoleID.ToString();
                    Session["Name"] = CurrentUser.Name.ToString();
                    ViewBag.Message = "Login Successfull";
                }
                else
                {
                    ViewBag.Alert = "Login Failed";
                }
            } 

            return View();
        }

        // UserProfile Page
        public ActionResult UserProfile(int id)
        {
            UserBL userBL = new UserBL();
            User user = userBL.GetUserByID(id);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UpdateUserVM>(); });
            IMapper iMapper = config.CreateMapper();
            UpdateUserVM userModel = iMapper.Map<User, UpdateUserVM>(user);
            ViewBag.WishList=userBL.GetUserWishlist(id);
            ViewBag.OrderList = userBL.GetUserOrderList(id);
            return View(userModel);
        }

        // Get Meathod for Edit User Profile 
        public ActionResult EditProfile(int id)
        {
            UserBL userBL = new UserBL();
            User user = userBL.GetUserByID(id);
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
                UserBL userBL = new UserBL();
               ViewBag.Alert= userBL.EditUser(user);
                if (ViewBag.Alert==null)
                {
                    ViewBag.Message = "Update Successfull";
                    ViewBag.Alert = null;
                }
            }
           
            return RedirectToAction("LogOut");
        }

        // Log out Meathod
        public ActionResult LogOut()
        {
            UserController.CurrentUser = null;
            Session["UserID"] = null;
            Session["RoleID"] = null;
            Session["Name"] = null;
            return RedirectToAction("LogIn");
        }

        // View Particular User Details
        public ActionResult ViewDetails(int id)
        {
            UserBL userBL = new UserBL();
            User user = userBL.GetUserByID(id);
            return View(user);
        }

        // Delete the User
        [AdminAuthorizationFilter]
        public ActionResult Delete(int id)
        {
            UserBL userBL = new UserBL();
            userBL.Delete(id);
            return RedirectToAction("Index");
        }

        // Add the Book Into the Users WishList
        public ActionResult AddToWishList(int id)
        {
            ViewData["WishList_Message"] = null;
            UserBL userBL = new UserBL();
            if (CurrentUser==null)
            {
                return RedirectToAction("LogIn");
            }
            int userID = CurrentUser.UserID;
            ViewData["WishList_Message"] = userBL.AddToWishList(userID, id);

            if (ViewData["WishList_Message"] == null)
                ViewData["WishList_Message"] = "Book added into the wishlist";
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Remove the Book From The Users WishList
        public ActionResult RemoveBookFormWishlist(int id)
        {
            UserBL userBL = new UserBL();
            userBL.RemoveBookFormWishlist(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Get The Particular Book Details From BookList
        public JsonResult GetBookDetails(int BookID)
        {
            BookBL bookBL = new BookBL();
            Book book = bookBL.GetBookDetails(BookID);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        // Add The Book Into the Users Orders
        public ActionResult AddToOrder(int id)
        {
            ViewData["OrderList_Message"] = null;
            UserBL userBL = new UserBL();

            if (CurrentUser == null)
            {
                return RedirectToAction("LogIn");
            }
            int userID = CurrentUser.UserID;
            ViewData["OrderList_Message"] = userBL.AddToOrder(userID,id);

            if (ViewData["OrderList_Message"] == null)
                ViewData["OrderList_Message"] = "Book added into the Order List";
            return Redirect(Request.UrlReferrer.ToString());
        }

        // Get the Book Details From Book List
        public JsonResult GetOrderedBookDetails(int BookID)
        {
            BookBL bookBL = new BookBL();
            Book book = bookBL.GetBookDetails(BookID);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}