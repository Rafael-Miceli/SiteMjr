using System.Data.Entity;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfCustomerServiceData.Context
{
    public class CustomerServiceContext : BaseContext<CustomerServiceContext>
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<CameraServiceType> CameraServices { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CameraServiceType>().ToTable("CameraServices");
        }
    }
}
