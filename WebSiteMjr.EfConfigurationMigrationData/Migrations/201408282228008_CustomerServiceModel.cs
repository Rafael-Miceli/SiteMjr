namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerServiceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        CallStatus = c.Int(nullable: false),
                        IsMostUrgent = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Company_Id = c.Int(),
                        ServiceType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceType_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.ServiceType_Id);
            
            CreateTable(
                "dbo.CameraServices",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceTypes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CameraServices", new[] { "Id" });
            DropIndex("dbo.Calls", new[] { "ServiceType_Id" });
            DropIndex("dbo.Calls", new[] { "Company_Id" });
            DropIndex("dbo.ServiceTypes", new[] { "Company_Id" });
            DropForeignKey("dbo.CameraServices", "Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.Calls", "ServiceType_Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.Calls", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ServiceTypes", "Company_Id", "dbo.Companies");
            DropTable("dbo.CameraServices");
            DropTable("dbo.Calls");
            DropTable("dbo.ServiceTypes");
        }
    }
}
