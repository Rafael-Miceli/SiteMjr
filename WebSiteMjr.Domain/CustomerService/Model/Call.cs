using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class Call: Key<Guid>
    {
        public Company Company { get; private set; }
        public IEnumerable<CompanyArea> CompanyAreas { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<Employee> EmployeeToResolve { get; private set; }
        public CallStatus CallStatus { get; private set; }
        public bool IsMostUrgent { get; private set; }
        public DateTime DateCreated { get; private set; }
        public ServiceType ServiceType { get; private set; }

        public Call(Company company, IEnumerable<CompanyArea> companyAreas,
            string title, ServiceType serviceType, bool isMostUrgent)
        {
            Company = company;
            CompanyAreas = companyAreas;
            Title = title;
            ServiceType = serviceType;
            IsMostUrgent = isMostUrgent;

            CallStatus = CallStatus.Open;
            DateCreated = DateTime.Now;
        }
    }

    public enum CallStatus
    {
        Open,
        Pendent,
        Attending,
        Closed,
        Cancelled
    }
}
