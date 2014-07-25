using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class EmployeeMapper
    {
        public Employee CreateEmployeeViewModelToEmployee(CreateEmployeeViewModel createEmployeeViewModel, Company employeeCompany)
        {
            return new Employee
            {
                Name = createEmployeeViewModel.Name,
                LastName = createEmployeeViewModel.LastName,
                Email = createEmployeeViewModel.Email,
                Phone = createEmployeeViewModel.Phone,
                Company = employeeCompany
            };
        }
    }
}
