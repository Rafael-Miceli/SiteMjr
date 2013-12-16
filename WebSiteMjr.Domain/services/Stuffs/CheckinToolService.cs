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
        private readonly IEmployeeService _employeeService;
        private readonly IToolService _toolService;

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork, ICompanyService companyService, IEmployeeService employeeService, IToolService toolService) 
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _employeeService = employeeService;
            _toolService = toolService;
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

        public IEnumerable<CheckinTool> FilterCheckins(string employeeCompanyName, string toolName, DateTime ?date)
        {
            var checkins = _checkinToolRepository.GetAll();

            if (!String.IsNullOrEmpty(employeeCompanyName))
            {
                checkins = checkins.Where(c => String.Equals(c.EmployeeCompanyHolder.Name, employeeCompanyName, StringComparison.CurrentCultureIgnoreCase));
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

        public IEnumerable<string> ListEmployeeCompanyHolderName()
        {
            return
                _companyService.ListCompany()
                    .Select(c => c.Name)
                    .Concat(_employeeService.ListEmployee().Select(e => e.Name));
        }

        public Holder FindEmployeeCompanyByName(string name)
        {
            Holder holder = _companyService.FindCompanyByName(name) ??
                            (Holder) _employeeService.FindEmployeeByName(name);

            return holder;
        }

        public IEnumerable<string> ListToolName()
        {
            return _toolService.ListTool().Select(t => t.Name);
        }

        public CheckinTool FindToolCheckin(object idCheckinTool)
        {
            return _checkinToolRepository.GetById(idCheckinTool);
        }
    }
}
