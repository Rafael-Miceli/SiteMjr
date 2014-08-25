using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class Call
    {
        public Company Company { get; private set; }
        public IEnumerable<CompanyArea> CompanyAreas { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<Employee> EmployeeToResolve { get; private set; }
        public CallStatus CallStatus { get; private set; }
        public DateTime DateCreated { get; private set; }
        public ServiceType ServiceType { get; private set; }

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
