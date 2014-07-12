using System.ComponentModel.DataAnnotations;

namespace WebSiteMjr.Domain.Interfaces.Model
{
    public interface INotDeletable
    {
        string Name { get; set; }
        [Required]
        bool IsDeleted { get; set; }
    }
}
