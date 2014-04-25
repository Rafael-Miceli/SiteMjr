using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;
using Mjr.Extensions;

namespace WebSiteMjr.Assembler
{
    public class CheckinToolMapper
    {
        private readonly ICheckinToolService _checkinToolService;
        private readonly IToolService _toolService;
        private readonly IHolderService _holderService;
        private readonly ICompanyAreasService _companyAreasService;

        public CheckinToolMapper(ICheckinToolService checkinToolService, IToolService toolService, IHolderService holderService, ICompanyAreasService companyAreasService)
        {
            _checkinToolService = checkinToolService;
            _toolService = toolService;
            _holderService = holderService;
            _companyAreasService = companyAreasService;
        }

        public ListCheckinToolViewModel GetChekinsFilter(ListCheckinToolViewModel checkinToolViewModel)
        {
            var checkins = _checkinToolService.FilterCheckins(_holderService.FindHolderByName(checkinToolViewModel.EmployeeCompanyHolder),
                                                                                   checkinToolViewModel.Tool,
                                                                                   checkinToolViewModel.CheckinDateTime);

            checkinToolViewModel.CheckinTools = CheckinToolToCreateCheckinToolViewModel(checkins).OrderByDescending(c => c.CheckinDateTime);


            return checkinToolViewModel;
        }

        private IEnumerable<EnumerableCheckinToolViewModel> CheckinToolToCreateCheckinToolViewModel(IEnumerable<CheckinTool> checkins)
        {
            var companyAreas = _companyAreasService.ListCompanyAreas().ToList();
            var holders = _holderService.ListHolder().ToList();

            foreach (var checkinTool in checkins)
            {
                var companyAreaName =  companyAreas.FirstOrDefault(c => c.Id == checkinTool.CompanyAreaId);

                yield return new EnumerableCheckinToolViewModel
                {
                    Id = checkinTool.Id,
                    CheckinDateTime = checkinTool.CheckinDateTime,
                    ToolName = checkinTool.Tool.Name,
                    EmployeeCompanyHolderName = holders.FirstOrDefault(h => h.Id == checkinTool.EmployeeCompanyHolderId).Name,
                    CompanyAreaName = companyAreaName != null ? companyAreaName.Name : null
                };
            }
        }

        public CheckinTool CreateCheckinToolViewModelToCheckinTool(CheckinToolViewModel createCheckinToolViewModel)
        {
            var tool = _toolService.FindToolByName(createCheckinToolViewModel.ToolName);
            var holder = _holderService.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName);
            var companyArea = _companyAreasService.FindCompanyAreaByName(createCheckinToolViewModel.CompanyAreaName);

            if (!holder.Exists())
                throw new ObjectNotExistsException<Holder>();

            if (!tool.Exists())
                throw new ObjectNotExistsException<Tool>();

            var checkinTool = new CheckinTool
            {
                Id = createCheckinToolViewModel.Id,
                EmployeeCompanyHolderId = holder.Id,
                Tool = tool,
                CheckinDateTime = createCheckinToolViewModel.CheckinDateTime.Value,
                CompanyAreaId = companyArea.Id
            };

            return checkinTool;
        }

        public CheckinToolViewModel CheckinToolToCreateCheckinToolViewModel(CheckinTool checkinTool)
        {
            var companyAreaName = _companyAreasService.FindCompanyArea(checkinTool.CompanyAreaId);

            var createCheckinToolViewModel = new CheckinToolViewModel
            {
                Id = checkinTool.Id,
                CheckinDateTime = checkinTool.CheckinDateTime,
                EmployeeCompanyHolderName = _holderService.FindHolder(checkinTool.EmployeeCompanyHolderId).Name,
                ToolName = checkinTool.Tool.Name,
                StrCheckinDateTime = checkinTool.CheckinDateTime.ToString(),
                CompanyAreaName = companyAreaName != null ? companyAreaName.Name : null
            };

            return createCheckinToolViewModel;
        }
    }
}
