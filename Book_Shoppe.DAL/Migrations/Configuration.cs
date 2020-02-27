namespace Book_Shoppe.DAL.Migrations
{
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Book_Shoppe.DAL.DBContext>
    {
        //public Configuration()
        //{
        //    AutomaticMigrationsEnabled = false;
        //}

        protected override void Seed(Book_Shoppe.DAL.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }

    public class DBContextInitializer : DropCreateDatabaseAlways<RoleDBContext>
    {
        protected override void Seed(RoleDBContext context)
        {
            List<Role> Roles = new List<Role>();

            Roles.Add(new Role() { RoleID = 1, RoleName = "Seller" });
            Roles.Add(new Role() { RoleID = 2, RoleName = "Customer" });
            context.Roles.AddRange(Roles);

            base.Seed(context);
        }
    }
}
