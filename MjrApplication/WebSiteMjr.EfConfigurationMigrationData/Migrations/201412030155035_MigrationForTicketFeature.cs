namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationForTicketFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calls", "Protocol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Calls", "Protocol");
        }
    }
}
