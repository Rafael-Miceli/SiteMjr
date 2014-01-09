namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToolLocalization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToolLocalizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ToolLocalizationCompanies",
                c => new
                    {
                        ToolLocalization_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ToolLocalization_Id, t.Company_Id })
                .ForeignKey("dbo.ToolLocalizations", t => t.ToolLocalization_Id, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.ToolLocalization_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ToolLocalizationCompanies", new[] { "Company_Id" });
            DropIndex("dbo.ToolLocalizationCompanies", new[] { "ToolLocalization_Id" });
            DropForeignKey("dbo.ToolLocalizationCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ToolLocalizationCompanies", "ToolLocalization_Id", "dbo.ToolLocalizations");
            DropTable("dbo.ToolLocalizationCompanies");
            DropTable("dbo.ToolLocalizations");
        }
    }
}
