using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfCustomerServiceData.Context;

namespace WebSiteMjr.EfCustomerServiceData.UnitOfWork
{
    public class CustomerServiceUow: UnitOfWork<CustomerServiceContext>
    {
        public CustomerServiceUow()
        {}

        public CustomerServiceUow(CustomerServiceContext customerServiceContext): base(customerServiceContext)
        {}
    }
}
