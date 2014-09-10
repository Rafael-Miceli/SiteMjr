namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedIsDeletedToHolder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holders", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Holders", "IsDeleted");
        }
    }
}
