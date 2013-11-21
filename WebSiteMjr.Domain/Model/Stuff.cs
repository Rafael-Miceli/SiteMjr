using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Stuff: IntId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual StuffCategory StuffCategory { get; set; }
        public virtual StuffManufacture StuffManufacture { get; set; }
    }
}
