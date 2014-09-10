namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyArea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyAreaCompanies",
                c => new
                    {
                        CompanyArea_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyArea_Id, t.Company_Id })
                .ForeignKey("dbo.CompanyAreas", t => t.CompanyArea_Id, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.CompanyArea_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CompanyAreaCompanies", new[] { "Company_Id" });
            DropIndex("dbo.CompanyAreaCompanies", new[] { "CompanyArea_Id" });
            DropForeignKey("dbo.CompanyAreaCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.CompanyAreaCompanies", "CompanyArea_Id", "dbo.CompanyAreas");
            DropTable("dbo.CompanyAreaCompanies");
            DropTable("dbo.CompanyAreas");
        }
    }
}
