namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_v5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        WishListID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishListID)
                .ForeignKey("dbo.Book", t => t.BookID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "UserID", "dbo.User");
            DropForeignKey("dbo.WishLists", "BookID", "dbo.Book");
            DropIndex("dbo.WishLists", new[] { "BookID" });
            DropIndex("dbo.WishLists", new[] { "UserID" });
            DropTable("dbo.WishLists");
        }
    }
}
