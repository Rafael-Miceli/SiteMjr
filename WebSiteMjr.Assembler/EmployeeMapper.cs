using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class EmployeeMapper
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeMapper(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Employee CreateEmployeeViewModelToEmployee(CreateEmployeeViewModel createEmployeeViewModel, Company employeeCompany)
        {
            if (EmailForEmployeeAlreadyExists(createEmployeeViewModel.Email))
                throw new EmployeeWithExistentEmailException();

            return new Employee
            {
                Name = createEmployeeViewModel.Name,
                LastName = createEmployeeViewModel.LastName,
                Email = createEmployeeViewModel.Email,
                Phone = createEmployeeViewModel.Phone,
                Company = employeeCompany
            };
        }

        private bool EmailForEmployeeAlreadyExists(string email)
        {
            return _employeeService.FindEmployeeByEmail(email) != null;
        }
    }
}
