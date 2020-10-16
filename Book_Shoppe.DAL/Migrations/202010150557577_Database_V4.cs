namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "CartRate", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "IsOrdered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "IsOrdered");
            DropColumn("dbo.Carts", "CartRate");
        }
    }
}
