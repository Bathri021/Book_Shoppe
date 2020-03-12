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
    [CustomeErrorHandler()]
    public class UserController : Controller
    {
        public static User CurrentUser = null;
        
      
        // GET: User
        [AdminAuthorizationFilter]
        public ActionResult Index()
        {
            UserBL userBL = new UserBL();
            IEnumerable<User> users = userBL.GetUsers();
            userBL.GetRoles();
            return View(users);
           
        }

        [HandleError]
        public ActionResult Register()
        {
            UserBL userBL = new UserBL();
            ViewBag.Roles = new SelectList(userBL.GetRoles(), "RoleID", "RoleName");
            return View();
        }

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

        public ActionResult LogIn()
        {
            return View();
        }

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

        public ActionResult UserProfile(int id)
        {
            UserBL userBL = new UserBL();
            User user = userBL.GetUserByID(id);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User,UpdateUserVM>(); });
            IMapper iMapper = config.CreateMapper();
            UpdateUserVM userModel = iMapper.Map<User, UpdateUserVM>(user);
            //UpdateUserVM userModel = new UpdateUserVM()
            //{
            //    UserID = user.UserID,
            //    Name = user.Name,
            //    UserName = user.UserName,
            //    MailID = user.MailID,
            //    Password = user.Password,
            //    RoleID = user.RoleID
            //};
            return View(userModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateUserVM userModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<UpdateUserVM,User>(); });
                IMapper iMapper = config.CreateMapper();
                User user = iMapper.Map<UpdateUserVM,User>(userModel);

                //User user = new Entity.User()
                //{
                //    UserID = userModel.UserID,
                //    Name = userModel.Name,
                //    UserName = userModel.UserName,
                //    MailID = userModel.MailID,
                //    Password = userModel.Password,
                //    RoleID = userModel.RoleID
                //};
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

        public ActionResult LogOut()
        {
            UserController.CurrentUser = null;
            Session["UserID"] = null;
            Session["RoleID"] = null;
            Session["Name"] = null;
            return RedirectToAction("LogIn");
        }
        public ActionResult ViewDetails(int id)
        {
            UserBL userBL = new UserBL();
            User user = userBL.GetUserByID(id);
            return View(user);
        }
        public ActionResult Delete(int id)
        {
            UserBL userBL = new UserBL();
            userBL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}