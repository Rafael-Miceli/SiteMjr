using System.Data.Entity;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfStuffData.Context
{
    public class StuffContext: BaseContext<StuffContext>
        {
            public DbSet<Stuff> Stuffs { get; set; }
            public DbSet<StuffCategory> StuffCategories { get; set; }
            public DbSet<StuffManufacture> StuffManufactures { get; set; }
    }
}
