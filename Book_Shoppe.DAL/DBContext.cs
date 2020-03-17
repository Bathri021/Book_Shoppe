﻿using Book_Shoppe.Entity;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<OrdersShipment> OrdersShipment { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.MailID).IsUnique();
            builder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<Book>().HasIndex(u => u.Title).IsUnique();
            builder.Entity<User>().MapToStoredProcedures();

            builder.Entity<OrdersShipment>().HasKey(os => os.OrdersShipmentID);
        }
    }
}
