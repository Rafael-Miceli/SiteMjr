namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAreaInCheckin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckinTools", "CompanyAreaId", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
