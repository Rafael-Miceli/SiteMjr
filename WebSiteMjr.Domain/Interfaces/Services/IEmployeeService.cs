using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(object employee);
        IEnumerable<Employee> ListEmployee();
        Employee FindEmployee(object idemployee);
        Employee FindEmployeeByName(string employeeName);
        IEnumerable<Employee> ListEmployeesNotDeleted();
        IEnumerable<Employee> ListEmployeesFromCompanyNotDeleted(int companyId);
    }
}