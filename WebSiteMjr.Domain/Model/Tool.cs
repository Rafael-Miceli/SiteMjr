using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Tool : IntId, IObjectWithState
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual StuffCategory StuffCategory { get; set; }
        public virtual StuffManufacture StuffManufacture { get; set; }
        [NotMapped]
        public State State { get; set; }
    }
}
