﻿using Book_Shoppe.App_Start;
using Book_Shoppe.BL;
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
        public static User CurrentUser { get; set; }
        
      
        // GET: User
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
                User _user = new Entity.User()
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    MailID =user.MailID,
                    Password = user.Password,
                    RoleID = user.RoleID
                };

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
        public ActionResult LogIn(LogInFormViewModel user)
        {
            UserBL userBL = new UserBL();
            if (ModelState.IsValid)
            {
                User _user = userBL.LogIn(user.UserName, user.Password);

                if (_user != null)
                {
                    UserController.CurrentUser = _user;
                    ViewBag.Message = "Login Successfull";
                }
                else
                {
                    ViewBag.Alert = "Login Failed";
                }
            } 

            return View();
        }

        public ActionResult LogOut()
        {
            UserController.CurrentUser = null;
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