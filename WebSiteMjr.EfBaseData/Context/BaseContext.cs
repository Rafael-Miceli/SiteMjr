using System.Data.Entity;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.EfBaseData.Context
{
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {

        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext() :
            base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
