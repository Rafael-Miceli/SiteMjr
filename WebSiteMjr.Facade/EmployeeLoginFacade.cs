using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Facade
{
    public class EmployeeLoginFacade
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMembershipService _membershipService;

        private readonly EmployeeMapper _employeeMapper;

        public EmployeeLoginFacade(IEmployeeService employeeService, IMembershipService membershipService)
        {
            _employeeService = employeeService;
            _membershipService = membershipService;
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
                    var password = _membershipService.CreateNewUserEmployeeAccount(employee);

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
    }
}
