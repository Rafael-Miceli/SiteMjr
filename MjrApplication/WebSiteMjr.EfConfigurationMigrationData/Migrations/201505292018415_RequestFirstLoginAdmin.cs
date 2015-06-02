namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestFirstLoginAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "GuidInSena", c => c.String());
            AddColumn("dbo.Users", "WantToResetPassword", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "WantToResetPassword");
            DropColumn("dbo.Companies", "GuidInSena");
        }
    }
}
