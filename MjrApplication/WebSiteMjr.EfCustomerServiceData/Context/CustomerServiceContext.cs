using System.Data.Entity;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfCustomerServiceData.Context
{
    public class CustomerServiceContext : BaseContext<CustomerServiceContext>
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<ServiceDetails> ServiceTypes { get; set; }
    }
}
