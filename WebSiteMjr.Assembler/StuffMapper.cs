using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class StuffMapper
    {
        public EditStuffsViewModel StuffToStuffViewModel(Stuff stuff)
        {
            var viewModel = new EditStuffsViewModel {Id = stuff.Id, Name = stuff.Name};
            if (stuff.StuffCategory != null)
                viewModel.SelectedStuffCategoryId = stuff.StuffCategory.Id;
            if (stuff.StuffManufacture != null)
                viewModel.SelectedStuffManufactureId = stuff.StuffManufacture.Id;

            return viewModel;
        }

        public Stuff StuffViewModelToStuff(EditStuffsViewModel stuffViewModel)
        {
            var stuff = new Stuff
            {
                Id = stuffViewModel.Id,
                Name = stuffViewModel.Name,
                StuffCategory = {Id = stuffViewModel.SelectedStuffCategoryId},
                StuffManufacture = {Id = stuffViewModel.SelectedStuffManufactureId}
            };

            return stuff;
        }
    }
}
