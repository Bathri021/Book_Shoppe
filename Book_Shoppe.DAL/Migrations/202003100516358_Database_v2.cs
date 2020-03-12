
namespace Book_Shoppe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_v2 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.User_Insert",
                p => new
                    {
                        RoleID = p.Int(),
                        Name = p.String(maxLength: 26),
                        UserName = p.String(maxLength: 26),
                        MailID = p.String(maxLength: 64),
                        Password = p.String(maxLength: 12),
                    },
                body:
                    @"INSERT [dbo].[User]([RoleID], [Name], [UserName], [MailID], [Password])
                      VALUES (@RoleID, @Name, @UserName, @MailID, @Password)
                      
                      DECLARE @UserID int
                      SELECT @UserID = [UserID]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [UserID] = scope_identity()
                      
                      SELECT t0.[UserID]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[UserID] = @UserID"
            );
            
            CreateStoredProcedure(
                "dbo.User_Update",
                p => new
                    {
                        UserID = p.Int(),
                        RoleID = p.Int(),
                        Name = p.String(maxLength: 26),
                        UserName = p.String(maxLength: 26),
                        MailID = p.String(maxLength: 64),
                        Password = p.String(maxLength: 12),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [RoleID] = @RoleID, [Name] = @Name, [UserName] = @UserName, [MailID] = @MailID, [Password] = @Password
                      WHERE ([UserID] = @UserID)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Delete",
                p => new
                    {
                        UserID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[User]
                      WHERE ([UserID] = @UserID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.User_Delete");
            DropStoredProcedure("dbo.User_Update");
            DropStoredProcedure("dbo.User_Insert");
        }
    }
}
