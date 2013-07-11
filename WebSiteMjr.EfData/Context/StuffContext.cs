using System.Data.Entity;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.EfData.Context
{
    public class StuffContext: BaseContext<StuffContext>
    {
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<StuffCategorie> StuffCategories { get; set; }
        public DbSet<StuffManufacture> StuffManufactures { get; set; }
    }
}
