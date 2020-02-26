using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.Entity
{
    public class User
    {
        public int UserID { get; set; }

        public int RoleID { get; set; }

        public Role Role { get; set; }

        public string Name { get; set; }

        
        public string UserName { get; set; }

        
        public string MailID { get; set; }

       
        public string Password { get; set; }

        
    

        public List<Book> Books { get; set; }

    }
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
