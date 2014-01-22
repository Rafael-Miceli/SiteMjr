using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class CheckinToolMapper
    {
        private readonly ICheckinToolService _checkinToolService;
        private readonly IToolService _toolService;
        private readonly IHolderService _holderService;

        public CheckinToolMapper(ICheckinToolService checkinToolService, IToolService toolService, IHolderService holderService)
        {
            _checkinToolService = checkinToolService;
            _toolService = toolService;
            _holderService = holderService;
        }

        public ListCheckinToolViewModel GetChekinsFilter(ListCheckinToolViewModel checkinToolViewModel)
        {
            var checkins = _checkinToolService.FilterCheckins(_holderService.FindHolderByName(checkinToolViewModel.EmployeeCompanyHolder),
                                                                                   checkinToolViewModel.Tool,
                                                                                   checkinToolViewModel.CheckinDateTime);

            checkinToolViewModel.CheckinTools = CheckinToolToCreateCheckinToolViewModel(checkins).OrderByDescending(c => c.CheckinDateTime);


            return checkinToolViewModel;
        }

        public IEnumerable<EnumerableCheckinToolViewModel> CheckinToolToCreateCheckinToolViewModel(IEnumerable<CheckinTool> checkins)
        {
            foreach (var checkinTool in checkins)
            {
                yield return new EnumerableCheckinToolViewModel
                {
                    Id = checkinTool.Id,
                    CheckinDateTime = checkinTool.CheckinDateTime,
                    ToolName = checkinTool.Tool.Name,
                    EmployeeCompanyHolderName = _holderService.FindHolder(checkinTool.EmployeeCompanyHolderId).Name
                };
            }
        }

        public CheckinTool CreateCheckinToolViewModelToCheckinTool(CreateCheckinToolViewModel createCheckinToolViewModel)
        {
            var tool = _toolService.FindToolByName(createCheckinToolViewModel.ToolName);
            var holder = _holderService.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName);

            if (!CompanyOrEmployeeExists(holder))
                throw new ObjectNotExistsException<Holder>();

            if (!ToolExists(tool))
                throw new ObjectNotExistsException<Tool>();


            var checkinTool = new CheckinTool
            {
                EmployeeCompanyHolderId = holder.Id,
                Tool = tool,
                CheckinDateTime = createCheckinToolViewModel.CheckinDateTime.Value
            };

            return checkinTool;
        }

        public CheckinTool EditingCreateCheckinToolViewModelToCheckinTool(int id, CreateCheckinToolViewModel createCheckinToolViewModel)
        {
            var tool = _toolService.FindToolByName(createCheckinToolViewModel.ToolName);
            var holder = _holderService.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName);
            var checkinTool = new CheckinTool();

            if (!CompanyOrEmployeeExists(holder))
                throw new ObjectNotExistsException<Holder>();

            if (!ToolExists(tool))
                throw new ObjectNotExistsException<Tool>();

            checkinTool.Id = createCheckinToolViewModel.Id;
            checkinTool.EmployeeCompanyHolderId = holder.Id;
            checkinTool.Tool = tool;
            checkinTool.CheckinDateTime = createCheckinToolViewModel.CheckinDateTime.Value;

            return checkinTool;
        }

        private bool ToolExists(Tool tool)
        {
            return tool != null;
        }

        private bool CompanyOrEmployeeExists(Holder employeeCompanyHolder)
        {
            return employeeCompanyHolder != null;
        }

        public CreateCheckinToolViewModel EditingCheckinToolToCreateCheckinToolViewModel(CheckinTool checkinTool)
        {
            var createCheckinToolViewModel = new CreateCheckinToolViewModel
            {
                Id = checkinTool.Id,
                CheckinDateTime = checkinTool.CheckinDateTime,
                EmployeeCompanyHolderName = _holderService.FindHolder(checkinTool.EmployeeCompanyHolderId).Name,
                ToolName = checkinTool.Tool.Name,
                StrCheckinDateTime = checkinTool.CheckinDateTime.ToString()
               
            };

            return createCheckinToolViewModel;
        }
    }
}
