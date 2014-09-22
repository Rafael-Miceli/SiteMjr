namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToolLocalizations", "Call_Id", "dbo.Calls");
            DropIndex("dbo.ToolLocalizations", new[] { "Call_Id" });
            AddColumn("dbo.Calls", "EmployeeThatCreatedId", c => c.Int(nullable: false));
            DropColumn("dbo.ToolLocalizations", "Call_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToolLocalizations", "Call_Id", c => c.Guid());
            DropColumn("dbo.Calls", "EmployeeThatCreatedId");
            CreateIndex("dbo.ToolLocalizations", "Call_Id");
            AddForeignKey("dbo.ToolLocalizations", "Call_Id", "dbo.Calls", "Id");
        }
    }
}
