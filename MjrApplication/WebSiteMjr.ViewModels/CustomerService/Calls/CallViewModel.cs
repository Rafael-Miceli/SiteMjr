using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;

namespace WebSiteMjr.ViewModels.CustomerService.Calls
{
    public class IndexCallViewModel
    {
        public IEnumerable<Call> CallsIntoService { get; set; }
        public IEnumerable<Call> CallsAwaiting { get; set; }
    }

    public class CreateCallViewModel
    {
        [Display(Prompt = "Título do chamado")]
        public string Title { get; set; }
        public ServiceDetails Details { get; set; }
        public IEnumerable<CompanyNameAndId> Companies { get; set; }
        public int SelectedCompanyId { get; set; }
        public IEnumerable<EmployeeNameAndId> EmployeesToResolve { get; set; }
        public IEnumerable<int> SelectedEmployeesToResolveId { get; set; }
        public bool IsMostUrgent { get; set; }
    }

    public class CompanyNameAndId
    {
        public string CompanyName { get; set; }
        public int Id { get; set; }
    }

    public class EmployeeNameAndId
    {
        public string EmployeeName { get; set; }
        public int Id { get; set; }
    }
}
