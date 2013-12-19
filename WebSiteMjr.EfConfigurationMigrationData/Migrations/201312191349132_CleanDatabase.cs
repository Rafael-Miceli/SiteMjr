namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanDatabase : DbMigration
    {
        public override void Up()
        {
            DropTable("EmployeesOld");
            DropTable("CompaniesOld");

            Sql("Delete from Stuffs");
        }
        
        public override void Down()
        {
        }
    }
}
