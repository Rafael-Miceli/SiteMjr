namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAreasInCheckins : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.CheckinTools", "CompanyArea_Id", c => c.Int());
            AddForeignKey("dbo.CheckinTools", "CompanyArea_Id", "dbo.ToolLocalizations", "Id");
            CreateIndex("dbo.CheckinTools", "CompanyArea_Id");
            //DropTable("dbo.ToolLocalizations");
            //DropTable("dbo.ToolLocalizationCompanies");
        }
        
        public override void Down()
        {
        }
    }
}
