namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Book", t => t.BookID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.OrdersShipments",
                c => new
                    {
                        OrdersShipmentID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ShipmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdersShipmentID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .ForeignKey("dbo.Shipments", t => t.ShipmentID)
                .Index(t => t.OrderID)
                .Index(t => t.ShipmentID);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        ShipmentID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ShipmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.User");
            DropForeignKey("dbo.OrdersShipments", "ShipmentID", "dbo.Shipments");
            DropForeignKey("dbo.OrdersShipments", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "BookID", "dbo.Book");
            DropIndex("dbo.OrdersShipments", new[] { "ShipmentID" });
            DropIndex("dbo.OrdersShipments", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "BookID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropTable("dbo.Shipments");
            DropTable("dbo.OrdersShipments");
            DropTable("dbo.Orders");
        }
    }
}
