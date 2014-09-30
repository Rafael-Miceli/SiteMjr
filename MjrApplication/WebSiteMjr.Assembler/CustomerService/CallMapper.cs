using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.ViewModels.CustomerService.Calls;

namespace WebSiteMjr.Assembler.CustomerService
{
    public class CallMapper
    {
        private readonly ICallService _callService;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;

        public CallMapper(ICallService callService, ICompanyService companyService, IEmployeeService employeeService)
        {
            _callService = callService;
            _companyService = companyService;
            _employeeService = employeeService;
        }

        public IndexCallViewModel GetIndexViewModel()
        {
            var allCallsToShowInIndex = GetCallsForIndex().ToList();

            var callsIntoService = allCallsToShowInIndex.Where(c => c.CallStatus == CallStatus.Attending);
            var callsAwaiting = allCallsToShowInIndex.Where(c => c.CallStatus != CallStatus.Attending);

            return new IndexCallViewModel
            {
                CallsAwaiting = callsAwaiting,
                CallsIntoService = callsIntoService
            };
        }

        private IEnumerable<Call> GetCallsForIndex()
        {
            return _callService.ListCalls()
                    .Where(c => c.CallStatus == CallStatus.Open || c.CallStatus == CallStatus.Attending || c.CallStatus == CallStatus.Pendent);
        }

        public Call FromCallViewModelToCall(CreateCallViewModel createCallViewModel)
        {
            var company = _companyService.FindCompany(createCallViewModel.SelectedCompanyId);
            var employeesToResolve = _employeeService.ListEmployee()
                                    .Where(c => createCallViewModel.SelectedEmployeesToResolveId.Contains(c.Id)).ToList();

            return new Call(company, createCallViewModel.Title, createCallViewModel.Details, createCallViewModel.IsMostUrgent, 1, employeesToResolve);
        }
    }
}
