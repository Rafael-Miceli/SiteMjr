using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class StuffMapper
    {
        private readonly IStuffCategoryService _stuffCategoryService;
        private readonly IStuffManufactureService _stuffManufactureService;

        public StuffMapper(IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService)
        {
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
        }

        public EditStuffsViewModel StuffToStuffViewModel(Stuff stuff)
        {
            var viewModel = new EditStuffsViewModel {Id = stuff.Id, Name = stuff.Name, Description = stuff.Description};
            if (stuff.StuffCategory != null)
                viewModel.StuffCategory = stuff.StuffCategory;
            if (stuff.StuffManufacture != null)
                viewModel.StuffManufacture = stuff.StuffManufacture;

            return viewModel;
        }

        public Stuff StuffViewModelToStuff(EditStuffsViewModel stuffViewModel)
        {
            var stuff = new Stuff
            {
                Id = stuffViewModel.Id,
                Name = stuffViewModel.Name,
                Description = stuffViewModel.Description,
                StuffCategory = _stuffCategoryService.FindStuffCategory(stuffViewModel.StuffCategoryId),
                StuffManufacture = _stuffManufactureService.FindStuffManufacture(stuffViewModel.StuffManufactureId)
            };

            return stuff;
        }
    }
}
