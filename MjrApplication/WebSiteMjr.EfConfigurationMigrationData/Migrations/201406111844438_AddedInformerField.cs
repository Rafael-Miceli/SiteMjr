namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInformerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckinTools", "Informer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckinTools", "Informer");
        }
    }
}
