using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICompanyService _companyService;

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork, ICompanyService companyService) 
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public void CheckinTool(CheckinTool checkinTool)
        {
            if (IsCheckinToolOfThisToolInCompany(checkinTool))
            {
                if (WasLastCheckinToolOfThisToolInCompany(checkinTool))
                {
                    return;
                }
            }
            _checkinToolRepository.Add(checkinTool);
            _unitOfWork.Save();
        }

        private bool WasLastCheckinToolOfThisToolInCompany(CheckinTool checkinTool)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(checkinTool);

            if (checkinsWithActualTool == null)
            {
                return false;
            }
            
            var lastCheckinToolBeforeTheActual =
                checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).FirstOrDefault(c => c.CheckinDateTime <= checkinTool.CheckinDateTime);

            return lastCheckinToolBeforeTheActual == null || IsCheckinToolOfThisToolInCompany(lastCheckinToolBeforeTheActual);
        }

        private IEnumerable<CheckinTool> ListCheckinToolsWithActualTool(CheckinTool checkinTool)
        {
            return _checkinToolRepository.Query(c => c.Tool.Id == checkinTool.Tool.Id);
        }


        private bool IsCheckinToolOfThisToolInCompany(CheckinTool checkinTool)
        {
            return _companyService.FindCompany(checkinTool.EmployeeCompanyHolderId) != null;
        }


        public void UpdateToolCheckin(CheckinTool checkinTool)
        {
            _checkinToolRepository.Update(checkinTool);
            _unitOfWork.Save();
        }
        
        public void DeleteToolCheckin(object idCheckinTool)
        {
            _checkinToolRepository.Remove(idCheckinTool);
            _unitOfWork.Save();
        }

        public IEnumerable<CheckinTool> ListToolCheckins()
        {
            return _checkinToolRepository.GetAll();
        }

        public IEnumerable<CheckinTool> FilterCheckins(Holder employeeCompany, string toolName, DateTime ?date)
        {
            var checkins = _checkinToolRepository.GetAll();

            if (employeeCompany != null)
            {
                checkins = checkins.Where(c => c.EmployeeCompanyHolderId == employeeCompany.Id);
            }

            if (!String.IsNullOrEmpty(toolName))
            {
                checkins = checkins.Where(c => String.Equals(c.Tool.Name, toolName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (date.HasValue)
            {
                checkins = checkins.Where(c => c.CheckinDateTime.Date == date.Value.Date);
            }

            return checkins;
        }

        public CheckinTool FindToolCheckin(object idCheckinTool)
        {
            return _checkinToolRepository.GetById(idCheckinTool);
        }
    }
}
