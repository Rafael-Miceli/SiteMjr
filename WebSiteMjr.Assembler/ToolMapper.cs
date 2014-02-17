using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class ToolMapper
    {
        private readonly IStuffCategoryService _stuffCategoryService;
        private readonly IStuffManufactureService _stuffManufactureService;
        private readonly IToolService _toolService;
        private readonly IHolderService _holderService;
        private readonly ICompanyAreasService _companyAreaService;
        private readonly ICheckinToolService _checkinToolService;

        public ToolMapper(IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService, IToolService toolService, IHolderService holderService, ICompanyAreasService companyAreaService, ICheckinToolService checkinToolService)
        {
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
            _toolService = toolService;
            _holderService = holderService;
            _companyAreaService = companyAreaService;
            _checkinToolService = checkinToolService;
        }

        public EditToolViewModel ToolToToolViewModel(Tool tool)
        {
            var viewModel = new EditToolViewModel {Id = tool.Id, Name = tool.Name, Description = tool.Description};
            if (tool.StuffCategory != null)
                viewModel.StuffCategory = tool.StuffCategory;
            if (tool.StuffManufacture != null)
                viewModel.StuffManufacture = tool.StuffManufacture;

            return viewModel;
        }

        public Tool ToolViewModelToTool(EditToolViewModel toolViewModel)
        {
            return toolViewModel.Id > 0 ? MapToolViewModelToUpdate(toolViewModel) : MapToolViewModelToCreate(toolViewModel);
        }

        private Tool MapToolViewModelToCreate(EditToolViewModel toolViewModel)
        {
            var tool = new Tool
            {
                Id = toolViewModel.Id,
                Name = toolViewModel.Name,
                Description = toolViewModel.Description,
                StuffCategory = _stuffCategoryService.FindStuffCategory(toolViewModel.StuffCategoryId),
                StuffManufacture = _stuffManufactureService.FindStuffManufacture(toolViewModel.StuffManufactureId)
            };
            return tool;
        }

        private Tool MapToolViewModelToUpdate(EditToolViewModel toolViewModel)
        {
            var tool = _toolService.FindTool(toolViewModel.Id);

            tool.Name = toolViewModel.Name;
            tool.Description = toolViewModel.Description;
            tool.StuffCategory = _stuffCategoryService.FindStuffCategory(toolViewModel.StuffCategoryId);
            tool.StuffManufacture = _stuffManufactureService.FindStuffManufacture(toolViewModel.StuffManufactureId);

            return tool;
        }

        public CheckinTool MapCheckinToolTabViewModelToCheckinTool(CheckinToolTabViewModel checkinToolTabViewModel)
        {
            var tool = _toolService.FindTool(checkinToolTabViewModel.ToolId);
            var holder = _holderService.FindHolderByName(checkinToolTabViewModel.HolderName);
            var companyArea = _companyAreaService.FindCompanyAreaByName(checkinToolTabViewModel.CompanyAreaName);

            int? companyAreaId;

            if (companyArea != null)
            {
                companyAreaId = companyArea.Id;
            }
            else
            {
                companyAreaId = null;
            }


            if (!HolderExists(holder))
                throw new ObjectNotExistsException<Holder>();
            

            var checkinTool = new CheckinTool
            {
                EmployeeCompanyHolderId = holder.Id,
                Tool = tool,
                CheckinDateTime = checkinToolTabViewModel.CheckinDateTime.Value,
                CompanyAreaId = companyAreaId
            };

            return checkinTool;
        }

        public CheckinToolTabViewModel CheckinsOfThisToolToCheckinToolTabViewModel(int toolId)
        {
            var thisToolCheckins = _checkinToolService.ListCheckinToolsWithActualTool(toolId);

            var checkinToolTabViewModel = new CheckinToolTabViewModel
            {
                CheckinTools = CheckinToolToEnumerableCheckinToolViewModel(thisToolCheckins),
                ToolId = toolId
            };

            if (checkinToolTabViewModel.CheckinTools != null)
                checkinToolTabViewModel.CheckinTools =
                    checkinToolTabViewModel.CheckinTools.OrderByDescending(c => c.CheckinDateTime);

            return checkinToolTabViewModel;
        }

        public IEnumerable<EnumerableCheckinToolViewModel> CheckinToolToEnumerableCheckinToolViewModel(IEnumerable<CheckinTool> checkins)
        {
            var companyAreas = _companyAreaService.ListCompanyAreas().ToList();
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

        private bool HolderExists(Holder employeeCompanyHolder)
        {
            return employeeCompanyHolder != null;
        }
    }
}
