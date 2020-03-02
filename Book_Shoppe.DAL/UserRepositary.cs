using Book_Shoppe.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            DBContext RoleContext = new DBContext();
            return RoleContext.Roles.Where(m => m.RoleID <=2).ToList();
        }
       
        public static string AddUser(User user)
        {
            DBContext _Context = new DBContext();
            _Context.Users.Add(user);
          
            try
            {
                _Context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.InnerException.Message != null)
                {
                    return "The User Name should not be duplicated";
                }
                else
                {
                    return "Please fill out the form correctly and sumbit your values";
                }
            }
            return null;
        }

        public static User ValidateLogIn(string userName,string password)
        {
            User _user=null;
            DBContext _Context = new DBContext();
            IList<User> userList = _Context.Users.ToList();

            foreach (User user in userList)
            {
                if (user.UserName ==userName && user.Password==password)
                {
                    _user = user;
                }
            }
            return _user;
        }
        public static User GetUserByID(int userID)
        {
            DBContext _context = new DBContext();
            return _context.Users.SingleOrDefault(ID => ID.UserID == userID);
        }
        public static bool Delete(int id)
        {
            using (var Context = new DBContext())
            {
                User user = Context.Users.Where(ID => ID.UserID == id).FirstOrDefault();
                Context.Users.Remove(user);
                Context.SaveChanges();
                return true;
            }
        }
}
}
