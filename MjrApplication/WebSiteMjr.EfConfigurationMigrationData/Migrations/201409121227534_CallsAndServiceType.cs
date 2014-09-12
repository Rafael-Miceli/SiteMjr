namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallsAndServiceType : DbMigration
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
                .ForeignKey("dbo.ServiceDetails", t => t.ServiceType_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.ServiceType_Id);
            
            CreateTable(
                "dbo.ServiceDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeCalls",
                c => new
                    {
                        Employee_Id = c.Int(nullable: false),
                        Call_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Employee_Id, t.Call_Id })
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .ForeignKey("dbo.Calls", t => t.Call_Id, cascadeDelete: true)
                .Index(t => t.Employee_Id)
                .Index(t => t.Call_Id);
            
            AddColumn("dbo.ToolLocalizations", "Call_Id", c => c.Guid());
            CreateIndex("dbo.ToolLocalizations", "Call_Id");
            AddForeignKey("dbo.ToolLocalizations", "Call_Id", "dbo.Calls", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calls", "ServiceType_Id", "dbo.ServiceDetails");
            DropForeignKey("dbo.ToolLocalizations", "Call_Id", "dbo.Calls");
            DropForeignKey("dbo.Calls", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.EmployeeCalls", "Call_Id", "dbo.Calls");
            DropForeignKey("dbo.EmployeeCalls", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.EmployeeCalls", new[] { "Call_Id" });
            DropIndex("dbo.EmployeeCalls", new[] { "Employee_Id" });
            DropIndex("dbo.ToolLocalizations", new[] { "Call_Id" });
            DropIndex("dbo.Calls", new[] { "ServiceType_Id" });
            DropIndex("dbo.Calls", new[] { "Company_Id" });
            DropColumn("dbo.ToolLocalizations", "Call_Id");
            DropTable("dbo.EmployeeCalls");
            DropTable("dbo.ServiceDetails");
            DropTable("dbo.Calls");
        }
    }
}
