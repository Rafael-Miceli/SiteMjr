namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MigrateStuffsToToolsTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Tools Select Name, Description, StuffCategory_Id, StuffManufacture_Id from Stuffs");
        }

        public override void Down()
        {
        }
    }
}
