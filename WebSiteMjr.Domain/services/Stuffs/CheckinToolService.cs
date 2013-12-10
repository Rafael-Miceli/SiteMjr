using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class CheckinToolService: ICheckinToolService
    {
        private readonly ICheckinToolRepository _checkinToolRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork)
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
        }

        public void CheckinTool(CheckinTool checkinTool)
        {
            throw new NotImplementedException();
        }

        public void UpdateToolCheckin(CheckinTool checkinTool)
        {
            throw new NotImplementedException();
        }

        public void DeleteToolCheckin(object checkinTool)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckinTool> ListToolCheckins()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckinTool> GetCheckinsByEmployeeName(string employeeName)
        {
            var checkinTool = new CheckinTool {Employee = new Employee {Name = employeeName}};
            yield return checkinTool;
            
        }


        public Tool FindToolCheckin(object idCheckinTool)
        {
            throw new NotImplementedException();
        }
    }
}
