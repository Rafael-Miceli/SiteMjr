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

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork)
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
        }

        public void CheckinTool(CheckinTool checkinTool)
        {
            _checkinToolRepository.Add(checkinTool);
            _unitOfWork.Save();
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

        public IEnumerable<CheckinTool> FilterCheckins(string employeeName, string toolName, DateTime ?date)
        {
            var checkins = _checkinToolRepository.GetAll();

            if (!String.IsNullOrEmpty(employeeName))
            {
                checkins = checkins.Where(c => c.Employee.Name == employeeName);
            }

            if (!String.IsNullOrEmpty(toolName))
            {
                checkins = checkins.Where(c => c.Tool.Name == toolName);
            }

            if (date != null)
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
