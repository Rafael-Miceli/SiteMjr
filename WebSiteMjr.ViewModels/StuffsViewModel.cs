using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class EditStuffsViewModel
    {
        public int Id { get; set; }
        public int? StuffCategoryId { get; set; }
        public int? StuffManufactureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StuffCategory StuffCategory { get; set; }
        public StuffManufacture StuffManufacture { get; set; }
    }
}
