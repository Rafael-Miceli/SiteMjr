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
        public CallStatus CallStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public ServiceType ServiceType { get; set; }

        public Call(Company company, IEnumerable<CompanyArea> companyAreas, string description,
            string title, ServiceType serviceType)
        {
            Company = company;
            CompanyAreas = companyAreas;
            Description = description;
            Title = title;
            ServiceType = serviceType;

            CallStatus = CallStatus.Open;
            DateCreated = DateTime.Now;
        }
    }

    public enum CallStatus
    {
        Open,
        Pendent,
        Attend,
        Closed
    }
}
