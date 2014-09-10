namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAreasInCheckins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckinTools", "ToolLocalizations_Id", c => c.Int());
            AddForeignKey("dbo.CheckinTools", "ToolLocalizations_Id", "dbo.ToolLocalizations", "Id");
            CreateIndex("dbo.CheckinTools", "ToolLocalizations_Id");
        }
        
        public override void Down()
        {
        }
    }
}
