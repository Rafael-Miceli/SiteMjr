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
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
            _unitOfWork.Save();
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
