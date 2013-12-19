namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RecreateCompaniesAndEmployeesTables : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Holders Select Name from CompaniesOld");
            Sql("Insert into Holders Select Name from EmployeesOld");

            Sql("ALTER TABLE CompaniesOld DROP COLUMN Name");
            Sql("ALTER TABLE EmployeesOld DROP COLUMN Name");

            Sql("Select * into Companies from CompaniesOld");
            Sql("Select Em.Id + (Select count(*) from Companies) as Id, Em.Phone, Em.LastName, Em.IdUser into Employees from EmployeesOld Em");

            Sql("ALTER TABLE Companies ALTER COLUMN Id INTEGER NOT NULL");
            Sql("ALTER TABLE Employees ALTER COLUMN Id INTEGER NOT NULL");

            AddPrimaryKey("Companies", "Id");
            AddPrimaryKey("Employees", "Id");

            AddForeignKey("Companies", "Id", "Holders");
            AddForeignKey("Employees", "Id", "Holders");
        }

        public override void Down()
        {
        }
    }
}
