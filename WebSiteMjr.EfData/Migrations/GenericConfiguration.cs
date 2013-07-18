using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WebSiteMjr.EfData.Migrations
{
    public class GenericConfiguration<T> : DbMigrationsConfiguration<T> where T : DbContext
    {
        public GenericConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
