namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAreaInCheckin : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.ToolLocalizationCompanies", "ToolLocalization_Id", "dbo.ToolLocalizations");
            //DropForeignKey("dbo.ToolLocalizationCompanies", "Company_Id", "dbo.Companies");
            //DropIndex("dbo.ToolLocalizationCompanies", new[] { "ToolLocalization_Id" });
            //DropIndex("dbo.ToolLocalizationCompanies", new[] { "Company_Id" });
            //CreateTable(
            //    "dbo.ToolLocalizations",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.ToolLocalizationCompanies",
            //    c => new
            //        {
            //            ToolLocalization_Id = c.Int(nullable: false),
            //            Company_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ToolLocalization_Id, t.Company_Id })
            //    .ForeignKey("dbo.ToolLocalizations", t => t.ToolLocalization_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
            //    .Index(t => t.ToolLocalization_Id)
            //    .Index(t => t.Company_Id);
            
            AddColumn("dbo.CheckinTools", "ToolLocalizations_Id", c => c.Int());
            AddForeignKey("dbo.CheckinTools", "ToolLocalizations_Id", "dbo.ToolLocalizations", "Id");
            CreateIndex("dbo.CheckinTools", "ToolLocalizations_Id");
            //DropTable("dbo.ToolLocalizations");
            //DropTable("dbo.ToolLocalizationCompanies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ToolLocalizationCompanies",
                c => new
                    {
                        ToolLocalization_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ToolLocalization_Id, t.Company_Id });
            
            CreateTable(
                "dbo.ToolLocalizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.ToolLocalizationCompanies", new[] { "Company_Id" });
            DropIndex("dbo.ToolLocalizationCompanies", new[] { "ToolLocalization_Id" });
            DropIndex("dbo.CheckinTools", new[] { "ToolLocalizations_Id" });
            DropForeignKey("dbo.ToolLocalizationCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ToolLocalizationCompanies", "ToolLocalization_Id", "dbo.ToolLocalizations");
            DropForeignKey("dbo.CheckinTools", "ToolLocalizations_Id", "dbo.ToolLocalizations");
            DropColumn("dbo.CheckinTools", "ToolLocalizations_Id");
            DropTable("dbo.ToolLocalizationCompanies");
            DropTable("dbo.ToolLocalizations");
            CreateIndex("dbo.ToolLocalizationCompanies", "Company_Id");
            CreateIndex("dbo.ToolLocalizationCompanies", "ToolLocalization_Id");
            AddForeignKey("dbo.ToolLocalizationCompanies", "Company_Id", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ToolLocalizationCompanies", "ToolLocalization_Id", "dbo.ToolLocalizations", "Id", cascadeDelete: true);
        }
    }
}
