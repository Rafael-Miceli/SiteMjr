namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCompanyAndEmployeeAndUserHierarchy : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TABLE Migration ( IdHolder int, EmailHolder char(100) ) Insert Into Migration (IdHolder, EmailHolder) Select Id, Email From Companies ");

            AddColumn("dbo.Users", "Employee_Id", c => c.Int());
            AddColumn("dbo.Holders", "Email", c => c.String());
            AddColumn("dbo.Employees", "Company_Id", c => c.Int());
            AddForeignKey("dbo.Users", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "Company_Id", "dbo.Companies", "Id");
            CreateIndex("dbo.Users", "Employee_Id");
            CreateIndex("dbo.Employees", "Company_Id");
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "IdCompany");
            DropColumn("dbo.Employees", "IdUser");
            DropColumn("dbo.Companies", "Email");

            Sql("DECLARE @ComId int, @ComEmail nvarchar(100) " +
                "DECLARE Company_Cursor CURSOR FOR " +
                "SELECT com.IdHolder, com.EmailHolder " +
                "FROM Migration com " +
                "OPEN Company_Cursor; " +
                "FETCH Company_Cursor INTO @ComId, @ComEmail " +
                "WHILE @@FETCH_STATUS = 0 " +
                "   BEGIN   		" +
	            "      update Holders Set Email = @ComEmail Where Id = @ComId " +
                "      FETCH Company_Cursor INTO @ComId, @ComEmail " +
                "   END; " +
                "CLOSE Company_Cursor; " +
                "DEALLOCATE Company_Cursor; " +
                "Update Employees Set Company_Id = 1"
                );
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Email", c => c.String());
            AddColumn("dbo.Employees", "IdUser", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "IdCompany", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "Name", c => c.String());
            DropIndex("dbo.Employees", new[] { "Company_Id" });
            DropIndex("dbo.Users", new[] { "Employee_Id" });
            DropForeignKey("dbo.Employees", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Users", "Employee_Id", "dbo.Employees");
            DropColumn("dbo.Employees", "Company_Id");
            DropColumn("dbo.Holders", "Email");
            DropColumn("dbo.Users", "Employee_Id");
        }
    }
}
