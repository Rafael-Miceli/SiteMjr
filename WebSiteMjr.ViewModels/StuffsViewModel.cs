using System.Collections.Generic;
using System.Web.Mvc;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class StuffsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<StuffCategorie> StuffCategorieId { get; set; }
        public IEnumerable<StuffManufacture> StuffManufactureId { get; set; }
        public IEnumerable<SelectListItem> StuffCategories { get; set; }
        public IEnumerable<SelectListItem> StuffManufactures { get; set; }
    }
}
