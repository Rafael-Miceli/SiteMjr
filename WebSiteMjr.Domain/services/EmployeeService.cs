using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMembershipService _membershipService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public EmployeeService(IEmployeeRepository employeeRepository, IMembershipService membershipService, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _membershipService = membershipService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        
        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
            _unitOfWork.Save();
        }

        public void CreateEmployeeAndLogin(Employee employee)
        {
            _employeeRepository.Add(employee);
            var password = _membershipService.CreateNewUserEmployeeAccount(employee);

            _unitOfWork.Save();

            SendLoginViaEmailToEmployee(password, employee);
        }

        private void SendLoginViaEmailToEmployee(string password, Employee employee)
        {
            _emailService.SendFirstLoginToEmployee(password, employee.Email, employee.Name, employee.LastName);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
            _unitOfWork.Save();
        }

        public void DeleteEmployee(object employee)
        {
            _employeeRepository.Remove(employee);
            _unitOfWork.Save();
        }

        public IEnumerable<Employee> ListEmployee()
        {
            return _employeeRepository.GetAll();
        }

        public Employee FindEmployee(object idemployee)
        {
            return _employeeRepository.GetById(idemployee);
        }

        public Employee FindEmployeeByName(string employeeName)
        {
            return _employeeRepository.GetEmployeeByName(employeeName);
        }

        public IEnumerable<Employee> ListEmployeesNotDeleted()
        {
            return _employeeRepository.GetAllEmployeesNotDeleted();
        }

        public IEnumerable<Employee> ListEmployeesFromCompanyNotDeleted(int companyId)
        {
            return _employeeRepository.GetAllEmployeesFromCompanyNotDeleted(companyId);
        }
    }
}
