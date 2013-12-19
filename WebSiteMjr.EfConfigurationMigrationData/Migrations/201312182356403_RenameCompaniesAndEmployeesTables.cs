namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameCompaniesAndEmployeesTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Companies");

            RenameTable("Employees", "EmployeesOld");
            RenameTable("Companies", "CompaniesOld");
        }

        public override void Down()
        {
        }
    }
}
