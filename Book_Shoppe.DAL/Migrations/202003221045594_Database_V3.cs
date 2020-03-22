namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_V3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.LanguageID)
                .Index(t => t.LanguageName, unique: true);
            
            AddColumn("dbo.Book", "LanguageID", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "LanguageID");
            AddForeignKey("dbo.Book", "LanguageID", "dbo.Languages", "LanguageID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "LanguageID", "dbo.Languages");
            DropIndex("dbo.Languages", new[] { "LanguageName" });
            DropIndex("dbo.Book", new[] { "LanguageID" });
            DropColumn("dbo.Book", "LanguageID");
            DropTable("dbo.Languages");
        }
    }
}
