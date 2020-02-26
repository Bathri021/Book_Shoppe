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
       

        public static IEnumerable<User> GetUsers()
        {
            UserDBContext userDBContext = new UserDBContext();
           return userDBContext.Users.ToList();
        }

        public static IEnumerable<Role> GetRoles()
        {
            RoleDBContext RoleContext = new RoleDBContext();
            return RoleContext.Roles.ToList();
        }
        public static bool AddUser(User user)
        {
            UserDBContext _Context = new UserDBContext();
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return true;
        }
}
}
