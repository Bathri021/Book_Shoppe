using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.Entity
{
    [Table("UserInfo")]
    public class User
    {
        [Key]
        [Column("UserID")]
        public int UserID { get; set; }

        [Column("RoleID")]
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("MailID")]
        public string MailID { get; set; }

       [Column("Password")]
        public string Password { get; set; }

        
    
        [NotMapped]
        public List<Book> Books { get; set; }

    }
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
