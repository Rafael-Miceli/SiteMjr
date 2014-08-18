using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Model.CustomerService
{
    public class Call
    {
        public Company Company { get; set; }
        public IEnumerable<CompanyArea> CompanyAreas { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public IEnumerable<Employee> EmployeeToResolve { get; set; }
        public CallStatus Type { get; set; }
        public DateTime DateCreated { get; set; }
        public ServiceType ProblemType { get; set; }
    }

    public enum CallStatus
    {
        Open,
        Pendent,
        Attend,
        Closed
    }
}
