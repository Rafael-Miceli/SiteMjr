using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels.CustomerService.Calls
{
    public class IndexCallViewModel
    {
        public IEnumerable<Call> CallsIntoService { get; set; }
        public IEnumerable<Call> CallsAwaiting { get; set; }
    }

    public class CreateCallViewModel
    {
        public string Title { get; set; }
        public ServiceDetails Details { get; set; }
        public IEnumerable<string> CompaniesNames { get; set; }
        public string SelectedCompany { get; set; }
        public IEnumerable<string> EmployeesToResolve { get; set; }
        public bool IsMostUrgent { get; set; }
    }
}
