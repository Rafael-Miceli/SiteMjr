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

        public ToolMapper(IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService, IToolService toolService)
        {
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
            _toolService = toolService;
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
    }
}
