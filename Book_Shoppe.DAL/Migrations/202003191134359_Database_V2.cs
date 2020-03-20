namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Genres", "GenreName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Genres", new[] { "GenreName" });
        }
    }
}
