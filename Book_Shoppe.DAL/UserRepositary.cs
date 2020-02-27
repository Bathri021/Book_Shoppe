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
            DBContext userDBContext = new DBContext();
            return userDBContext.Users.ToList();
        }

        public static IEnumerable<Role> GetRoles()
        {
            RoleDBContext RoleContext = new RoleDBContext();
            return RoleContext.Roles.ToList();
        }
        public static IEnumerable<Book> GetBooks()
        {
            DBContext BookDBContext = new DBContext();
            return BookDBContext.Books.ToList();
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
