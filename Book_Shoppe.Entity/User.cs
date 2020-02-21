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

        [Required]
        [StringLength(22)]
        public string Name { get; set; }

        [Required]
        [StringLength(12,MinimumLength =5)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string MailID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Role")]
        [Required]
        public int RoleID { get; set; }

    }
    public class Roles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
