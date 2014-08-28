using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.ViewModels.CustomerService.Calls;

namespace WebSiteMjr.Assembler.CustomerService
{
    public class CallMapper
    {
        private readonly ICallService _callService;

        public CallMapper(ICallService callService)
        {
            _callService = callService;
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
            return
                _callService.ListCalls()
                    .Where(
                        c =>
                            c.CallStatus == CallStatus.Open || c.CallStatus == CallStatus.Attending ||
                            c.CallStatus == CallStatus.Pendent);
        }
    }
}
