namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Books", "Title", unique: true);
            CreateIndex("dbo.User", "UserName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "UserName" });
            DropIndex("dbo.Books", new[] { "Title" });
        }
    }
}
