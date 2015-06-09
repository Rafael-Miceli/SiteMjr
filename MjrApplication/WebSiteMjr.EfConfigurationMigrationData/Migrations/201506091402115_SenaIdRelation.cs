namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenaIdRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserSenaId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserSenaId");
        }
    }
}
