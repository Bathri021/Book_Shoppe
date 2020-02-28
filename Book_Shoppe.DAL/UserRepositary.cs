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
        static void InitialzeRoles()
        {
            List<Role> Roles = new List<Role>();
            Roles.Add(new Role() { RoleID = 1, RoleName = "Seller" });
            Roles.Add(new Role() { RoleID = 2, RoleName = "Customer" });

            DBContext _Context = new DBContext();
            _Context.Roles.AddRange(Roles);
            _Context.SaveChanges();
        }
        public static IEnumerable<User> GetUsers()
        {
            DBContext userDBContext = new DBContext();
            return userDBContext.Users.ToList();
        }

        public static IEnumerable<Role> GetRoles()
        {
            InitialzeRoles();
            DBContext RoleContext = new DBContext();
            return RoleContext.Roles.ToList();
        }
       
        public static bool AddUser(User user)
        {
            DBContext _Context = new DBContext();
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return true;
        }
}
}
