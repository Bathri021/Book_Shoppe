using Book_Shoppe.DAL.Migrations;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public class DBContext : DbContext
    {
        public DBContext():base("Book_Shoppe")
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
   
    public class RoleDBContext : DbContext
    {
        public RoleDBContext():base("Book_Shoppe")
        {
            Database.SetInitializer(new DBContextInitializer());
        }
        public DbSet<Role> Roles { get; set; }
    }
   

   
}
