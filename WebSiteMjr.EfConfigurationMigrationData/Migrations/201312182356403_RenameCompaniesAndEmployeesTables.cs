namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameCompaniesAndEmployeesTables : DbMigration
    {
        public override void Up()
        {

            RenameTable("Employees", "EmployeesOld");
            RenameTable("Companies", "CompaniesOld");
        }

        public override void Down()
        {
        }
    }
}
