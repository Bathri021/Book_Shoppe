namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_v3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Genre", newName: "Genres");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.Role", newName: "Roles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Roles", newName: "Role");
            RenameTable(name: "dbo.Users", newName: "User");
            RenameTable(name: "dbo.Genres", newName: "Genre");
        }
    }
}
