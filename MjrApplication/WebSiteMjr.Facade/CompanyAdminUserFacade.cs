using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Facade
{
    public class CompanyAdminUserFacade : ICompanyAdminUserFacade
    {
        private readonly ICompanyService _companyService;
        private readonly IMembershipService _membershipService;
        private readonly ISenaClientService _senaClientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly IEmailService _emailService;

        public CompanyAdminUserFacade(ICompanyService companyService, IMembershipService membershipService, IEmployeeService employeeService,
            IEmailService emailService, ISenaClientService senaClientService, IUnitOfWork unitOfWork)
        {
            _companyService = companyService;
            _membershipService = membershipService;
            _senaClientService = senaClientService;
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            _emailService = emailService;
        }

        public async Task CreateAdminUserForCompany(int companyId)
        {
            //Get this company data to create the new admin user
            Company company = _companyService.FindCompany(companyId);

            await CreateCompanyInSena(company);

            CreateAdminUserForCompanyInSena(company);
        }

        public async Task CreateCompanyInSena(Company company)
        {
            //Create the company in Sena
            var companyName = RemoveSpecialCharacters(company.Name); 
            await _senaClientService.Create(companyName);
            string clientGuid = await _senaClientService.FindByName(companyName);
            //Update GuidInSena with new company in Sena guid
            company.GuidInSena = clientGuid;
            _companyService.UpdateCompany(company);
            _unitOfWork.Save();
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", "", RegexOptions.Compiled);
        }

        public void CreateAdminUserForCompanyInSena(Company company)
        {
            //Create a new Employee for company
            var newEmployee = new Employee
            {
                Name = "Administrador",
                LastName = "Empresa",
                Email = company.Email,
                Company = company
            };
            _employeeService.CreateEmployee(newEmployee);

            //Insert a new user without password from this Company(Client) in Azure Mobile Table
            //Will not insert user here anymore, as will be a default administrator user
            //Will add only in the crate password moment
            //_senaUserService.Create(company.Email, company.Name);

            //Get the new user GUID
            //string userGuid = _senaUserService.FindByEmail(company.Email);

            //Insert a new user in MjrSqlTable            
            //Create a "Create new password request" for the user
            User user = new User {Employee = newEmployee, Username = company.Email, WantToResetPassword = true};
            _membershipService.CreateCompanyAdminAccount(user);
            _unitOfWork.Save();

            var userRegistered = _membershipService.GetLoggedUser(user.Username);

            //Send Email to new User
            _emailService.SendCreatePasswordRequest(newEmployee.Name, newEmployee.Email, userRegistered.Id);
        }
        
    }
}
