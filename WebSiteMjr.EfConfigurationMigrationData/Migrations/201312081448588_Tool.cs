namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tools",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Description = c.String(),
                    StuffCategory_Id = c.Int(),
                    StuffManufacture_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StuffCategories", t => t.StuffCategory_Id)
                .ForeignKey("dbo.StuffManufactures", t => t.StuffManufacture_Id)
                .Index(t => t.StuffCategory_Id)
                .Index(t => t.StuffManufacture_Id);
        }
        
        public override void Down()
        {
        }
    }
}
