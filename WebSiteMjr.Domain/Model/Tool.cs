using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Tool : IntId, IObjectWithState, IMjrException, INotDeletable
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public virtual StuffCategory StuffCategory { get; set; }
        public virtual StuffManufacture StuffManufacture { get; set; }
        [NotMapped]
        public State State { get; set; }
        [NotMapped]
        public string ObjectName
        {
            get
            {
                return "Ferramenta";
            }
        }
    }
}
