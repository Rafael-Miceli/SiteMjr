using System.Collections.Generic;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Facade
{
    public interface IEmployeeLoginFacade
    {
        void CreateEmployeeAndLogin(CreateEmployeeViewModel employeeViewModel, Company employeeCompany);
        User GetLoggedUser(string name);
        void CreateNewUserForExistentEmployeeAccount(Employee employee);
        IEnumerable<Employee> ListEmployeesFromCompanyNotDeleted(int companyId);
        void CreateEmployee(CreateEmployeeViewModel employee, Company employeeCompany);
        void UpdateEmployee(Employee employee);
        Employee FindEmployee(object idemployee);
        void DeleteEmployee(object employee);
    }
}