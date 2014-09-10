namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersAndRolesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });

            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            

            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id });
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            CreateIndex("dbo.RoleUsers", "User_Id");
            CreateIndex("dbo.RoleUsers", "Role_Id");
            AddForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
