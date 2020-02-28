namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 55),
                        Author = c.String(nullable: false, maxLength: 26),
                        Genere = c.String(nullable: false, maxLength: 20),
                        Price = c.Int(nullable: false),
                        NoOfPages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
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
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Roles");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Books", new[] { "UserID" });
            DropTable("dbo.Roles");
            DropTable("dbo.User");
            DropTable("dbo.Books");
        }
    }
}
