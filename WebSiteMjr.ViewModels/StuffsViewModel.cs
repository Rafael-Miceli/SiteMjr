using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class StuffsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StuffCategorie StuffCategorieId { get; set; }
        public StuffManufacture StuffManufactureId { get; set; }
        public IEnumerable<StuffCategorie> StuffCategories { get; set; }
        public IEnumerable<StuffManufacture> StuffManufactures { get; set; }
    }
}
