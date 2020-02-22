using Book_Shoppe.Entity;
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
            userList.Add(new User {UserID=1, Name = "Bathri", UserName = "Bathri", MailID = "bathri@gmail.com",Password="12345", RoleID = 1 });
            userList.Add(new User {UserID=2 ,Name = "Vinoth", UserName = "Vinoth", MailID = "vinoth@gmail.com", Password = "12345", RoleID = 2 });
            userList.Add(new User { UserID=3,Name = "Ragav", UserName = "Ragav", MailID = "ragav@gmail.com", Password = "12345", RoleID = 3});
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
            userValue.Name = user.Name;
            userValue.UserName = user.UserName;
            userValue.MailID = user.MailID;
            userValue.Password = user.Password;
            userValue.RoleID = user.RoleID;
        }

        public static User ValidateLogIn(string userName,string password)
        {
            foreach (User user in userList)
            {
                if (user.UserName==userName&&user.Password==password)
                    return user;
                return null;
            }
            return null;
        }
    }
}
