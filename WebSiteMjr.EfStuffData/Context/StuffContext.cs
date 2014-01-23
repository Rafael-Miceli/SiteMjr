using System.Data.Entity;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfStuffData.Context
{
    public class StuffContext: BaseContext<StuffContext>
    {
        public DbSet<Tool> Tools { get; set; }
        //public DbSet<CompanyArea> CompanyAreas { get; set; }
        public DbSet<CheckinTool> CheckinTools { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<StuffCategory> StuffCategories { get; set; }
        public DbSet<StuffManufacture> StuffManufactures { get; set; }
    }
}
