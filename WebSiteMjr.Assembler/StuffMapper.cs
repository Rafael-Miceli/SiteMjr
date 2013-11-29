using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class StuffMapper
    {
        private readonly IStuffCategoryService _stuffCategoryService;
        private readonly IStuffManufactureService _stuffManufactureService;
        private readonly IStuffService _stuffService;

        public StuffMapper(IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService, IStuffService stuffService)
        {
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
            _stuffService = stuffService;
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
            return stuffViewModel.Id > 0 ? MapStuffViewModelToUpdate(stuffViewModel) : MapStuffViewModelToCreate(stuffViewModel);
        }

        private Stuff MapStuffViewModelToCreate(EditStuffsViewModel stuffViewModel)
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

        private Stuff MapStuffViewModelToUpdate(EditStuffsViewModel stuffViewModel)
        {
            var stuff = _stuffService.FindStuff(stuffViewModel.Id);

            stuff.Name = stuffViewModel.Name;
            stuff.Description = stuffViewModel.Description;
            stuff.StuffCategory = _stuffCategoryService.FindStuffCategory(stuffViewModel.StuffCategoryId);
            stuff.StuffManufacture = _stuffManufactureService.FindStuffManufacture(stuffViewModel.StuffManufactureId);

            return stuff;
        }
    }
}
