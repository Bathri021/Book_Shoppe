using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Shoppe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<User> users = UserRepositary.GetUsers();
            return View(users);
        }

        public ActionResult Register()
        {
         //   ViewBag.Roles = new SelectList(UserRepositary.GetAllRoles(), "RoleID", "RoleName");
         //   ViewBag.userCount = UserRepositary.getUserCount() + 1;
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "UserID,Name,UserName,MailID,Password,RoleID")]User user)
        {        
          //  ViewBag.Roles = new SelectList(UserRepositary.GetAllRoles(), "RoleID", "RoleName");
            //ViewBag.userCount = UserRepositary.getUserCount() + 1;
            //if (string.IsNullOrEmpty(user.RoleID.ToString()))
            //{
            //    ModelState.AddModelError("RoleID", "Select The Role");
            //}
            //if (!ModelState.IsValid)
            //{
            //    return View("Register", user);
            //}else
            //{
            //    UserRepositary.Add(user);
            //    ViewBag.Message = "Registration Successfull";
            //}
           
            return View(user);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(FormCollection form)
        {
        //    if (form!=null)
        //    {
        //        User currentUser = UserRepositary.ValidateLogIn(form["UserName"], form["Password"]);

        //        if (currentUser != null)
        //        {
        //            Content("LogIn Sucessfull");
        //        }
        //    }
          
            return View();
        }
    }
}