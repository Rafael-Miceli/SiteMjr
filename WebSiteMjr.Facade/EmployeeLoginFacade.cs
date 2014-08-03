using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Facade
{
    public class EmployeeLoginFacade : IEmployeeLoginFacade
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMembershipService _membershipService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly EmployeeMapper _employeeMapper;

        public EmployeeLoginFacade(IEmployeeService employeeService, IMembershipService membershipService, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _membershipService = membershipService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _employeeMapper = new EmployeeMapper();
        }

        public void CreateEmployeeAndLogin(CreateEmployeeViewModel employeeViewModel, Company employeeCompany)
        {
            try
            {
                var employee = _employeeMapper.CreateEmployeeViewModelToEmployee(employeeViewModel, employeeCompany);

                using (var scope = new TransactionScope())
                {
                    _employeeService.CreateEmployee(employee);
                    var password = _membershipService.CreateAccountAndReturnPassword(CreateNewUserEmployeeAccount(employee), employee.Company.IsMjrCompany());

                    _unitOfWork.Save();

                    SendLoginViaEmailToEmployee(password, employee);

                    scope.Complete();
                }

            }
            catch (FlexMembershipException ex)
            {
                throw new EmployeeWithExistentEmailException();
            }
        }

        private User CreateNewUserEmployeeAccount(Employee employee)
        {
            return new User
            {
                IsLocal = true,
                Username = employee.Email,
                StatusUser = StatusUser.Active,
                Employee = employee
            };
        }

        public User GetLoggedUser(string name)
        {
            return _membershipService.GetLoggedUser(name);
        }

        public void CreateNewUserForExistentEmployeeAccount(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> ListEmployeesFromCompanyNotDeleted(int companyId)
        {
            return _employeeService.ListEmployeesFromCompanyNotDeleted(companyId);
        }

        public void CreateEmployee(CreateEmployeeViewModel employee, Company employeeCompany)
        {
            _employeeService.CreateEmployee(_employeeMapper.CreateEmployeeViewModelToEmployee(employee, employeeCompany));
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
        }

        public Employee FindEmployee(object idemployee)
        {
            return _employeeService.FindEmployee(idemployee);
        }

        public void DeleteEmployee(object employeeId)
        {
            _employeeService.DeleteEmployee(employeeId);
        }

        private void SendLoginViaEmailToEmployee(string password, Employee employee)
        {
            _emailService.SendFirstLoginToEmployee(password, employee.Email, employee.Name, employee.LastName);
        }
    }
}
