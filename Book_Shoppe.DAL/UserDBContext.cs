using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    class UserDBContext : DbContext
    {
        public UserDBContext():base("Context")
        {

        }        
        public DbSet<User> Users { get; set; }
       
    }
    class BooksDBContext : DbContext
    {
        public BooksDBContext():base("Context")
        {

        }
        public DbSet<Book> Books { get; set; }
    }

    class RoleContextInitializer : DropCreateDatabaseAlways<RoleDBContext>
    {
        protected override void Seed(RoleDBContext context)
        {
            List<Role> Roles = new List<Role>();

            Roles.Add(new Role() { RoleID = 1, RoleName = "Seller" });
            Roles.Add(new Role() { RoleID = 2, RoleName = "Customer" });
            base.Seed(context);
        }
    }

    public class RoleDBContext : DbContext
    {
        public RoleDBContext() : base("Context")
        {
            Database.SetInitializer<RoleDBContext>(new RoleContextInitializer());
        }
        public DbSet<Role> Roles { get; set; }
    }
    
}
