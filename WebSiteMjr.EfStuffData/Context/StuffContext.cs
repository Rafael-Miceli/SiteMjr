using System.Data.Entity;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfStuffData.Context
{
    public class StuffContext: BaseContext<StuffContext>
    {
        public DbSet<Tool> Tools { get; set; }
        public DbSet<CheckinTool> CheckinTools { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<StuffCategory> StuffCategories { get; set; }
        public DbSet<StuffManufacture> StuffManufactures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CompanyArea>().ToTable("ToolLocalizations");

            modelBuilder.Entity<CheckinTool>()
                .HasOptional(c => c.CompanyArea)
                .WithMany()
                .Map(m => m.MapKey("ToolLocalizations_Id"));

            //modelBuilder.Entity<CheckinTool>()
            //    .HasRequired(c => c.CompanyArea)
            //    .WithOptional()
            //    .Map(m => m.MapKey("ToolLocalizations_Id"));
        }
    }
}
