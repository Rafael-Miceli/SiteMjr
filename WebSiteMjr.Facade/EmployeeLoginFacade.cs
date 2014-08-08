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
    public abstract class CreateOrUpdateEmployeeTemplate
    {
        public virtual void CreateOrUpdateEmployee_CreateLogin(Employee employee, Func<Employee, string> createOrUpdateFunc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    CreateOrUpdateEmployee(employee, createOrUpdateFunc);

                    var password = CreateAccount(employee);

                    Commit();

                    SendLoginViaEmailToEmployee(password, employee);

                    scope.Complete();
                }

            }
            catch (FlexMembershipException ex)
            {
                throw new EmployeeWithExistentEmailException();
            }
        }

        protected abstract void SendLoginViaEmailToEmployee(string password, Employee employee);

        protected abstract string CreateAccount(Employee employee);

        protected abstract void Commit();

        protected abstract void CreateOrUpdateEmployee(Employee employee, Func<Employee, string> createOrUpdateFunc);
    }

    public class EmployeeLoginFacade : CreateOrUpdateEmployeeTemplate, IEmployeeLoginFacade
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
            var employee = _employeeMapper.CreateEmployeeViewModelToEmployee(employeeViewModel, employeeCompany);

            CreateOrUpdateEmployee_CreateLogin(employee, CreateEmployee);
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
            var employeeInstance = _employeeService.FindEmployee(employee.Id);

            CreateOrUpdateEmployee_CreateLogin(employeeInstance, UpdateEmployee);
        }

        public IEnumerable<Employee> ListEmployeesFromCompanyNotDeleted(int companyId)
        {
            return _employeeService.ListEmployeesFromCompanyNotDeleted(companyId);
        }

        public void CreateEmployee(CreateEmployeeViewModel employee, Company employeeCompany)
        {
            _employeeService.CreateEmployee(_employeeMapper.CreateEmployeeViewModelToEmployee(employee, employeeCompany));
        }

        public string UpdateEmployee(Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
            return null;
        }

        public Employee FindEmployee(object idemployee)
        {
            return _employeeService.FindEmployee(idemployee);
        }

        public void DeleteEmployee(object employeeId)
        {
            using (var scope = new TransactionScope())
            {
                _employeeService.DeleteEmployee(employeeId);

                var user = _membershipService.FindActiveUserByEmployeeId((int) employeeId);

                _membershipService.InactiveUser(user);

                Commit();

                scope.Complete();
            }
        }

        protected override void SendLoginViaEmailToEmployee(string password, Employee employee)
        {
            _emailService.SendFirstLoginToEmployee(password, employee.Email, employee.Name, employee.LastName);
        }

        protected override string CreateAccount(Employee employee)
        {
            return _membershipService.CreateAccountAndReturnPassword(CreateNewUserEmployeeAccount(employee), employee.Company.IsMjrCompany());
        }

        protected override void Commit()
        {
            _unitOfWork.Save();
        }

        protected override void CreateOrUpdateEmployee(Employee employee, Func<Employee, string> createOrUpdateFunc)
        {
            createOrUpdateFunc(employee);
        }

        private string CreateEmployee(Employee employee)
        {
            _employeeService.CreateEmployee(employee);
            return null;
        }
    }
}
