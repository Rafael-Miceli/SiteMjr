namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RecreateCompaniesAndEmployeesTables : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Holders Select Name from Companies");
            Sql("Insert into Holders Select Name from Employees");

            Sql("CREATE TABLE #CompanyDestination (                                                          " +
	            "        Id int NOT NULL,                                                                    " +
	            "        [Email] nvarchar(max),                                                              " +
	            "        [Address] nvarchar(max),                                                            " +
	            "        [City] nvarchar(max),                                                               " +
	            "        [Phone] nvarchar(max)                                                               " +
                "    )                                                                                       " +
                "                                                                                            " +
                "                                                                                            " +
                "                                                                                            " +
                "    INSERT INTO #CompanyDestination (                                                       " +
	            "        Id,                                                                                 " +
	            "        [Email],                                                                            " +
	            "        [Address],                                                                          " +
	            "        [City],                                                                             " +
	            "        [Phone]                                                                             " +
                "    )SELECT Id, Email, Address, City, Phone                                                 " +
                "    FROM dbo.Companies;                                                                     " +
                "                                                                                            " +
                "                                                                                            " +
                "                                                                                            " +
                "    CREATE TABLE #EmployeeDestination (                                                     " +
	            "        Id int NOT NULL,                                                                    " +
	            "        [LastName] nvarchar(max),                                                           " +
	            "        [Phone] nvarchar(max),                                                              " +
                "        [IdUser] int                                                                        " +
                "    )                                                                                       " +
                "                                                                                            " +
                "                                                                                            " +
                "                                                                                            " +
                "    INSERT INTO #EmployeeDestination (                                                      " +
	            "        Id,                                                                                 " +
	            "        [LastName],                                                                         " +
	            "        [Phone],                                                                            " +
                "        [IdUser]                                                                            " +
                "    )SELECT em.Id + (SELECT COUNT(*) FROM dbo.Companies) AS Id, LastName, Phone, IdUser     " +
                "    FROM dbo.Employees em                                                                   " +
                "                                                                                            " +
                "                                                                                            " +
                "                                                                                            " +
                "    DROP TABLE dbo.Companies                                                                " +
                "    DROP TABLE dbo.Employees                                                                " +
                "                                                                                            " +
                "                                                                                            " +
                "    CREATE TABLE Companies (                                                                " +
	            "        Id int NOT NULL,                                                                    " +
	            "        [Email] nvarchar(max),                                                              " +
	            "        [Address] nvarchar(max),                                                            " +
	            "        [City] nvarchar(max),                                                               " +
	            "        [Phone] nvarchar(max)                                                               " +
                "                                                                                            " +
	            "        CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED                                     " +
	            "        (                                                                                   " +
		        "            [Id] ASC                                                                        " +
	            "        )                                                                                   " +
                "    )                                                                                       " +
                "                                                                                            " +
                "    CREATE TABLE Employees (                                                                " +
	            "        Id int NOT NULL,                                                                    " +
	            "        [LastName] nvarchar(max),                                                           " +
	            "        [Phone] nvarchar(max),                                                              " +
                "        [IdUser] int                                                                        " +
                "                                                                                            " +
	            "        CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED                                     " +
	            "        (                                                                                   " +
		        "            [Id] ASC                                                                        " +
	            "        )                                                                                   " +
                "    )                                                                                       " +
                "                                                                                            " +
                "                                                                                            " +
                "    INSERT INTO Companies (                                                                 " +
	            "        Id,                                                                                 " +
	            "        [Email],                                                                            " +
	            "        [Address],                                                                          " +
	            "        [City],                                                                             " +
	            "        [Phone]                                                                             " +
                "    )SELECT Id, Email, Address, City, Phone                                                 " +
                "    FROM #CompanyDestination;                                                               " +
                "                                                                                            " +
                "    INSERT INTO Employees (                                                                 " +
	            "        Id,                                                                                 " +
	            "        [LastName],                                                                         " +
	            "        [Phone],                                                                            " +
                "        [IdUser]                                                                            " +
                "    )SELECT Id, LastName, Phone, IdUser                                                     " +
                "    FROM #EmployeeDestination                                                               " +
                "                                                                                            " +
                "                                                                                            " +
                "    DROP TABLE #CompanyDestination                                                          " +
                "    DROP TABLE #EmployeeDestination                                                         ");
            
            AddForeignKey("Companies", "Id", "Holders");
            AddForeignKey("Employees", "Id", "Holders");
        }

        public override void Down()
        {
        }
    }
}
