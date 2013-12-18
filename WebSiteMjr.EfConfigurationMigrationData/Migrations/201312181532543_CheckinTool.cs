//namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
    
//    public partial class CheckinTool : DbMigration
//    {
//        public override void Up()
//        {

//            DropTable("__MigrationHistory");

//            CreateTable(
//                "dbo.Holders",
//                c => new
//                {
//                    Id = c.Int(nullable: false, identity: true),
//                    Name = c.String(nullable: false),
//                })
//                .PrimaryKey(t => t.Id);



//            RenameTable("Companies", "CompanyTeste");
//            RenameTable("Employees", "EmployeeTeste");

//            //AddForeignKey("Companies", "Id", "Holders");

//            //AddForeignKey("Employees", "Id", "Holders");

//            CreateTable(
//                "dbo.Employee",
//                c => new
//                    {
//                        Id = c.Int(nullable: false),
//                        Phone = c.String(),
//                        LastName = c.String(nullable: false),
//                        IdUser = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.Holders", t => t.Id)
//                .Index(t => t.Id);

//            CreateTable(
//                "dbo.Company",
//                c => new
//                    {
//                        Id = c.Int(nullable: false),
//                        Email = c.String(),
//                        Address = c.String(),
//                        City = c.String(),
//                        Phone = c.String(),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.Holders", t => t.Id)
//                .Index(t => t.Id);
            

//            CreateTable(
//                "dbo.Tools",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        Name = c.String(),
//                        Description = c.String(),
//                        StuffCategory_Id = c.Int(),
//                        StuffManufacture_Id = c.Int(),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.StuffCategories", t => t.StuffCategory_Id)
//                .ForeignKey("dbo.StuffManufactures", t => t.StuffManufacture_Id)
//                .Index(t => t.StuffCategory_Id)
//                .Index(t => t.StuffManufacture_Id);


//            CreateTable(
//               "dbo.CheckinTools",
//               c => new
//               {
//                   Id = c.Int(nullable: false, identity: true),
//                   EmployeeCompanyHolderId = c.Int(nullable: false),
//                   CheckinDateTime = c.DateTime(nullable: false),
//                   Tool_Id = c.Int(),
//               })
//               .PrimaryKey(t => t.Id)
//               .ForeignKey("dbo.Tools", t => t.Tool_Id)
//               .Index(t => t.Tool_Id);

            
//            //CreateTable(
//            //    "dbo.Users",
//            //    c => new
//            //        {
//            //            Id = c.Int(nullable: false, identity: true),
//            //            Name = c.String(),
//            //            LastName = c.String(),
//            //            Email = c.String(),
//            //            StatusUser = c.Int(nullable: false),
//            //            IdCompany = c.Int(nullable: false),
//            //            Username = c.String(),
//            //            Password = c.String(),
//            //            Salt = c.String(),
//            //            PasswordResetToken = c.String(),
//            //            PasswordResetTokenExpiration = c.DateTime(nullable: false),
//            //            IsLocal = c.Boolean(nullable: false),
//            //        })
//            //    .PrimaryKey(t => t.Id);
            
//            //CreateTable(
//            //    "dbo.Roles",
//            //    c => new
//            //        {
//            //            Id = c.Int(nullable: false, identity: true),
//            //            Name = c.String(),
//            //        })
//            //    .PrimaryKey(t => t.Id);

            
            
//            //CreateTable(
//            //    "dbo.Stuffs",
//            //    c => new
//            //        {
//            //            Id = c.Int(nullable: false, identity: true),
//            //            Name = c.String(),
//            //            Description = c.String(),
//            //            StuffCategory_Id = c.Int(),
//            //            StuffManufacture_Id = c.Int(),
//            //        })
//            //    .PrimaryKey(t => t.Id)
//            //    .ForeignKey("dbo.StuffCategories", t => t.StuffCategory_Id)
//            //    .ForeignKey("dbo.StuffManufactures", t => t.StuffManufacture_Id)
//            //    .Index(t => t.StuffCategory_Id)
//            //    .Index(t => t.StuffManufacture_Id);
            
//            //CreateTable(
//            //    "dbo.StuffCategories",
//            //    c => new
//            //        {
//            //            Id = c.Int(nullable: false, identity: true),
//            //            Name = c.String(nullable: false),
//            //        })
//            //    .PrimaryKey(t => t.Id);
            
//            //CreateTable(
//            //    "dbo.StuffManufactures",
//            //    c => new
//            //        {
//            //            Id = c.Int(nullable: false, identity: true),
//            //            Name = c.String(nullable: false),
//            //        })
//            //    .PrimaryKey(t => t.Id);

//           //CreateTable(
//            //    "dbo.RoleUsers",
//            //    c => new
//            //        {
//            //            Role_Id = c.Int(nullable: false),
//            //            User_Id = c.Int(nullable: false),
//            //        })
//            //    .PrimaryKey(t => new { t.Role_Id, t.User_Id })
//            //    .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
//            //    .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
//            //    .Index(t => t.Role_Id)
//            //    .Index(t => t.User_Id);
            
//        }
        
//        public override void Down()
//        {
//            //DropIndex("dbo.Company", new[] { "Id" });
//            //DropIndex("dbo.Employee", new[] { "Id" });
//            //DropIndex("dbo.RoleUsers", new[] { "User_Id" });
//            //DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
//            //DropIndex("dbo.CheckinTools", new[] { "Tool_Id" });
//            //DropIndex("dbo.Tools", new[] { "StuffManufacture_Id" });
//            //DropIndex("dbo.Tools", new[] { "StuffCategory_Id" });
//            //DropIndex("dbo.Stuffs", new[] { "StuffManufacture_Id" });
//            //DropIndex("dbo.Stuffs", new[] { "StuffCategory_Id" });
//            //DropForeignKey("dbo.Company", "Id", "dbo.Holders");
//            //DropForeignKey("dbo.Employee", "Id", "dbo.Holders");
//            //DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
//            //DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
//            //DropForeignKey("dbo.CheckinTools", "Tool_Id", "dbo.Tools");
//            //DropForeignKey("dbo.Tools", "StuffManufacture_Id", "dbo.StuffManufactures");
//            //DropForeignKey("dbo.Tools", "StuffCategory_Id", "dbo.StuffCategories");
//            //DropForeignKey("dbo.Stuffs", "StuffManufacture_Id", "dbo.StuffManufactures");
//            //DropForeignKey("dbo.Stuffs", "StuffCategory_Id", "dbo.StuffCategories");
//            //DropTable("dbo.Company");
//            //DropTable("dbo.Employee");
//            //DropTable("dbo.RoleUsers");
//            //DropTable("dbo.CheckinTools");
//            //DropTable("dbo.Tools");
//            //DropTable("dbo.StuffManufactures");
//            //DropTable("dbo.StuffCategories");
//            //DropTable("dbo.Stuffs");
//            //DropTable("dbo.Holders");
//            //DropTable("dbo.Roles");
//            //DropTable("dbo.Users");
//        }
//    }
//}
