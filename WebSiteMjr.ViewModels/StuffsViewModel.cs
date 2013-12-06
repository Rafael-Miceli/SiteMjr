using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class EditStuffsViewModel
    {
        public int Id { get; set; }
        public int? StuffCategoryId { get; set; }
        public int? StuffManufactureId { get; set; }
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Name { get; set; }
        public string Description { get; set; }
        public StuffCategory StuffCategory { get; set; }
        public StuffManufacture StuffManufacture { get; set; }
    }
}
