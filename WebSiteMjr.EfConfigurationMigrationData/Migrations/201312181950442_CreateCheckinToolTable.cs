namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCheckinToolTable : DbMigration
    {
        public override void Up()
        {

            CreateTable(
               "dbo.CheckinTools",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   EmployeeCompanyHolderId = c.Int(nullable: false),
                   CheckinDateTime = c.DateTime(nullable: false),
                   Tool_Id = c.Int(),
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Tools", t => t.Tool_Id)
               .Index(t => t.Tool_Id);


            //DropForeignKey("dbo.Employee", "Id", "dbo.Holders");
            //DropForeignKey("dbo.Company", "Id", "dbo.Holders");
            //DropIndex("dbo.Employee", new[] { "Id" });
            //DropIndex("dbo.Company", new[] { "Id" });
            //CreateTable(
            //    "dbo.Employees",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Phone = c.String(),
            //            LastName = c.String(nullable: false),
            //            IdUser = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Holders", t => t.Id)
            //    .Index(t => t.Id);
            
            //CreateTable(
            //    "dbo.Companies",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Email = c.String(),
            //            Address = c.String(),
            //            City = c.String(),
            //            Phone = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Holders", t => t.Id)
            //    .Index(t => t.Id);
            
            //DropTable("dbo.Employee");
            //DropTable("dbo.Company");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.Company",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Email = c.String(),
            //            Address = c.String(),
            //            City = c.String(),
            //            Phone = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Employee",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Phone = c.String(),
            //            LastName = c.String(nullable: false),
            //            IdUser = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //DropIndex("dbo.Companies", new[] { "Id" });
            //DropIndex("dbo.Employees", new[] { "Id" });
            //DropForeignKey("dbo.Companies", "Id", "dbo.Holders");
            //DropForeignKey("dbo.Employees", "Id", "dbo.Holders");
            //DropTable("dbo.Companies");
            //DropTable("dbo.Employees");
            //CreateIndex("dbo.Company", "Id");
            //CreateIndex("dbo.Employee", "Id");
            //AddForeignKey("dbo.Company", "Id", "dbo.Holders", "Id");
            //AddForeignKey("dbo.Employee", "Id", "dbo.Holders", "Id");
        }
    }
}
