namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceTypeAndCall : DbMigration
    {
        public override void Up()
        {
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
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.CameraServers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChannelName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.CameraServices", "Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.Calls", "ServiceType_Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.Calls", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ServiceTypes", "Company_Id", "dbo.Companies");
            DropIndex("dbo.CameraServices", new[] { "Id" });
            DropIndex("dbo.ServiceTypes", new[] { "Company_Id" });
            DropIndex("dbo.Calls", new[] { "ServiceType_Id" });
            DropIndex("dbo.Calls", new[] { "Company_Id" });
            DropTable("dbo.CameraServices");
            DropTable("dbo.Channels");
            DropTable("dbo.CameraServers");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Calls");
        }
    }
}
