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
       public static List<User> GetUsers()
        {
            Context userDBContext = new Context();
           return userDBContext.Users.ToList();
        }
}
}
