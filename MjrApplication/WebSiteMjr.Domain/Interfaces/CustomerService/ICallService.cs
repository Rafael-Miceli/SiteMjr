using System.Collections.Generic;
using WebSiteMjr.Domain.CustomerService.Model;

namespace WebSiteMjr.Domain.Interfaces.CustomerService
{
    public interface ICallService
    {
        void CreateCall(Call call);
        IEnumerable<Call> ListCalls();
    }
}