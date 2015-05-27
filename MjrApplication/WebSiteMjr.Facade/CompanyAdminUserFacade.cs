using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Facade
{
    public class CompanyAdminUserFacade
    {
        private readonly ICompanyService _companyService;
        private readonly IMembershipService _membershipService;
        private readonly ISenaUserService _senaUserService;
        private readonly ISenaClientService _senaClientService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmailService _emailService;

        public CompanyAdminUserFacade(ICompanyService companyService, IMembershipService membershipService, IEmployeeService employeeService,
            IEmailService emailService, ISenaUserService senaUserService, ISenaClientService senaClientService)
        {
            _companyService = companyService;
            _senaUserService = senaUserService;
            _membershipService = membershipService;
            _senaClientService = senaClientService;
            _employeeService = employeeService;
            _emailService = emailService;
        }

        public void CreateAdminUserForCompany(int companyId)
        {
            try
            {
                
                //Get this company data to create the new admin user
                Company company = _companyService.FindCompany(companyId);

                //Create the company in Sena
                _senaClientService.Create(company.Name);
                string clientGuid = _senaClientService.FindByName(company.Name);
                //Update GuidInSena with new company in Sena guid
                company.GuidInSena = clientGuid;
                _companyService.UpdateCompany(company);

                //Create a new Employee for company
                var newEmployee = new Employee { Name = "Administrador", Email = company.Email, Company = company };
                _employeeService.CreateEmployee(newEmployee);

                //Insert a new user without password from this Company(Client) in Azure Mobile Table
                _senaUserService.Add(company.Email, company.Name);

                //Get the new user GUID
                string userGuid = _senaUserService.FindByEmail(company.Email);

                //Insert a new user in MjrSqlTable            
                //Create a "Create new password request" for the user
                User user = new User { Employee = newEmployee, Username = company.Email, WantToResetPassword = true };
                _membershipService.CreateCompanyAdminAccount(user);

                //Send Email to new User
                _emailService.SendCreatePasswordRequest(newEmployee.Name, newEmployee.Email);

            }
            catch (Exception)
            {

                throw;
            }
        }

        //WindowsAzure.MobileServices 
    }
}
