﻿using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public class UserRepositary
    {
        public static List<User> userList = new List<User>();
        public static List<Roles> rolesList = new List<Roles>();

        static UserRepositary()
        {
            userList.Add(new User { Name = "Bathri", UserName = "Bathri", MailID = "bathri@gmail.com", RoleID = 1 });
            userList.Add(new User { Name = "Vinoth", UserName = "Vinoth", MailID = "vinoth@gmail.com", RoleID = 2 });
            userList.Add(new User { Name = "Ragav", UserName = "Ragav", MailID = "ragav@gmail.com", RoleID = 3});
            rolesList.Add(new Roles { RoleID = 1, RoleName = "Admin" });
            rolesList.Add(new Roles { RoleID = 2, RoleName = "Seller" });
            rolesList.Add(new Roles { RoleID = 3, RoleName = "Customer" });
        }
        public static IEnumerable<User> GetAllUsers()
        {
            return userList;
        }
        public static IEnumerable<Roles> GetAllRoles()
        {
            return rolesList;
        }
        public static int getUserCount()
        {
            int count = userList.Count();
            return count;
        }
        public static void Add(User user)
        {
            userList.Add(user);
        }
        public static void Updata(User user)
        {
            User userValue = userList.Find(id => id.UserID == user.UserID);
            userValue.UserID = userList.Count() + 1;
            userValue.Name = user.Name;
            userValue.UserName = user.UserName;
            userValue.MailID = user.MailID;
            userValue.Password = user.Password;
            userValue.RoleID = user.RoleID;
        }
    }
}
