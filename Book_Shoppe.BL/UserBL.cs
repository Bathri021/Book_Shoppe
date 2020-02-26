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
        public bool AddUser(User user)
        {
            return UserRepositary.AddUser(user);
        }
    }
}
