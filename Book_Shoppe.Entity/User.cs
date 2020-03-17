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

       
        [Required]
        [MaxLength(26)]
        public string Name { get; set; }

       
        [Required]
        [MaxLength(26)]
        public string UserName { get; set; }


        [Required]
        [MaxLength(64)]
        public string MailID { get; set; }

 
        [Required]
        [MaxLength(12)]
        public string Password { get; set; }


        
        public List<WishList> WishLists { get; set; }
    }


    public class Role
    {
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string RoleName { get; set; }
    }

    public class WishList
    {
        public int WishListID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public ICollection<OrdersShipment> OrdersShipments { get; set; }
    }

    public class Shipment
    {
        public int ShipmentID { get; set; }


        public string Address { get; set; }

        public ICollection<OrdersShipment> OrdersShipments { get; set; }
    }

    public class OrdersShipment
    {
        public int OrdersShipmentID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ShipmentID { get; set; }
        public Shipment Shipment { get; set; }
    }
}
