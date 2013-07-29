using System.Collections.Generic;
using System.Web.Mvc;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class CreateStuffsViewModel
    {
        private IEnumerable<StuffCategory> _stuffCategories;
        private IEnumerable<StuffManufacture> _stuffManufactures;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> StuffCategories
        {
            get
            {
                return new SelectList(_stuffCategories, "Id", "Name");
            }
        }

        public IEnumerable<SelectListItem> StuffManufactures
        {
            get
            {
                return new SelectList(_stuffManufactures, "Id", "Name");
            }
        }
    }

    public class EditStuffsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SelectedStuffCategoryId { get; set; }
        public int SelectedStuffManufactureId { get; set; }
        public virtual StuffCategory StuffCategory { get; set; }
        public virtual StuffManufacture StuffManufacture { get; set; }
        public IEnumerable<SelectListItem> StuffCategories { get; set; }
        public IEnumerable<SelectListItem> StuffManufactures { get; set; }
    }
}
