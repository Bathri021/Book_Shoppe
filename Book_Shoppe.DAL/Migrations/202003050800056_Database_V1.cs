namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 55),
                        Author = c.String(nullable: false, maxLength: 26),
                        GenreID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        NoOfPages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Genre", t => t.GenreID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.Title, unique: true)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 26),
                        UserName = c.String(nullable: false, maxLength: 26),
                        MailID = c.String(nullable: false, maxLength: 64),
                        Password = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.MailID, unique: true);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Book", "GenreID", "dbo.Genre");
            DropIndex("dbo.User", new[] { "MailID" });
            DropIndex("dbo.User", new[] { "UserName" });
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Book", new[] { "GenreID" });
            DropIndex("dbo.Book", new[] { "Title" });
            DropIndex("dbo.Book", new[] { "UserID" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Genre");
            DropTable("dbo.Book");
        }
    }
}
