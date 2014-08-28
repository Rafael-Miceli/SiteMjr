using System.Collections.Generic;
using SharedKernel;
using WebSiteMjr.Domain.CustomerService.Events;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Interfaces.CustomerService.Repository;
using WebSiteMjr.Domain.Interfaces.Uow;

namespace WebSiteMjr.Domain.CustomerService.Services
{
    public class CallService : ICallService
    {
        private readonly ICallRepository _callRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CallService(ICallRepository callRepository, IUnitOfWork unitOfWork)
        {
            _callRepository = callRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCall(Call call)
        {
            _callRepository.Add(call);
            _unitOfWork.Save();

            DomainEvents.Raise(new CallAddedEvent(call));
        }

        public IEnumerable<Call> ListCalls()
        {
            return _callRepository.GetAll();
        }
    }
}