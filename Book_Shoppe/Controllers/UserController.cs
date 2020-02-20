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
            IEnumerable<User> users = UserRepositary.GetAllUsers();
            return View(users);
        }

        public ActionResult Register()
        {
            ViewBag.userCount = UserRepositary.getUserCount();
            ViewBag.Roles = new SelectList(UserRepositary.GetAllRoles(), "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(/*[Bind(Include ="UserID,Name,UserName,MailID,Password,RoleID")]*/User user)
        {
            ViewBag.Roles = new SelectList(UserRepositary.GetAllRoles(), "RoleID", "RoleName");
            if (!ModelState.IsValidField("UserName"))
            {
                return View("Register", user);
            }
            UserRepositary.Add(user);
            ViewBag.Message = "Registration Successfull";
            return RedirectToAction("Register");
        }
    }
}