namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        Genere = c.String(),
                        Price = c.Int(nullable: false),
                        NoOfPages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.UserInfo", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        Name = c.String(),
                        UserName = c.String(),
                        MailID = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "UserID", "dbo.UserInfo");
            DropForeignKey("dbo.UserInfo", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserInfo", new[] { "RoleID" });
            DropIndex("dbo.Books", new[] { "UserID" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserInfo");
            DropTable("dbo.Books");
        }
    }
}
