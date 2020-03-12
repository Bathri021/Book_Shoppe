using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.BL
{
    public class UserBL
    {
        private User CurrentUser;
        public User GetCurrentUser()
        {
            return CurrentUser;
        }
        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            UserRepositary.SetCurrentUser(CurrentUser);
        }
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> Users = UserRepositary.GetUsers();
            return Users;
        }
        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> Roles = UserRepositary.GetRoles();
            return Roles;
        }
        public string AddUser(User user)
        {
            IEnumerable<User> Users = UserRepositary.GetUsers();
            bool duplications=false;
            foreach (var item in Users)
            {
               if( item.UserName == user.UserName)
                {
                    duplications = true;
                } 
            }
            if (!duplications)
                return UserRepositary.AddUser(user);
            else
                return "User Name Should Not Be Duplicated";
        }
        public User LogIn(string userName,string password)
        {
            return UserRepositary.ValidateLogIn(userName, password);
        }

        public User GetUserByID(int id)
        {
            return UserRepositary.GetUserByID(id);
        }

        public string EditUser(User user)
        {
            return UserRepositary.EditUser(user);
        }

        public bool Delete(int id)
        {
            return UserRepositary.Delete(id);
        }
    }
}
