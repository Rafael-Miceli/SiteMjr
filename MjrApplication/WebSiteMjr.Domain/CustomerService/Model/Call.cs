using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class Call: Key<Guid>
    {
        public Company Company { get; private set; }
        public string Title { get; private set; }
        public CallStatus CallStatus { get; private set; }
        public bool IsMostUrgent { get; private set; }
        public DateTime DateCreated { get; private set; }
        public ServiceDetails ServiceType { get; private set; }


        private ICollection<CompanyArea> _companyAreas;
        public virtual ICollection<CompanyArea> CompanyAreas
        {
            get
            {
                return _companyAreas ?? (_companyAreas = new Collection<CompanyArea>());
            }
            private set
            {
                _companyAreas = value;
            }
        }

        private ICollection<Employee> _employeesToResolve;
        public virtual ICollection<Employee> EmployeesToResolve
        {
            get
            {
                return _employeesToResolve ?? (_employeesToResolve = new Collection<Employee>());
            }
            private set
            {
                _employeesToResolve = value;
            }
        }

        public Call(Company company, ICollection<CompanyArea> companyAreas,
            string title, ServiceDetails serviceType, bool isMostUrgent)
        {
            base.Id = Guid.NewGuid();
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
