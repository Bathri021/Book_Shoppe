using Book_Shoppe.App_Start;
using Book_Shoppe.BL;
using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using Book_Shoppe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Shoppe.Controllers
{
    [CustomeErrorHandler()]
    public class UserController : Controller
    {
        UserBL userBL = new UserBL();
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<User> users = userBL.GetUsers();
            return View(users);
           
        }

        [HandleError]
        public ActionResult Register()
        {
            ViewBag.Roles = new SelectList(userBL.GetRoles(), "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationFormViewModel user)
        {
            ViewBag.Roles = new SelectList(userBL.GetRoles(), "RoleID", "RoleName");
           
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }
            else
            {
                User _user = new Entity.User()
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    MailID =user.MailID,
                    Password = user.Password,
                    RoleID = user.RoleID
                };
                if(userBL.AddUser(_user))
                 ViewBag.Message = "Registration Successfull";
            }

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