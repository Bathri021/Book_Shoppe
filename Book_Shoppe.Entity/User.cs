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
    [Table("User")]
    public class User
    {
        [Key]
        [Column("UserID")]
        [Required]
        public int UserID { get; set; }

        [Column("RoleID")]
        [Required]
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [Column("Name")]
        [Required]
        [MaxLength(26)]
        public string Name { get; set; }

        [Column("UserName")]
        [Required]
        [MaxLength(26)]
        public string UserName { get; set; }

        [Column("MailID")]
        [Required]
        [MaxLength(64)]
        public string MailID { get; set; }

        [Column("Password")]
        [Required]
        [MaxLength(12)]
        public string Password { get; set; }


        [NotMapped]
        public List<Book> Books { get; set; }

    }

    [Table("Role")]
    public class Role
    {
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
