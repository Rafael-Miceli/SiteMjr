using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Stuff: IntId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StuffCategorie StuffCategorie { get; set; }
        public StuffManufacture StuffManufacture { get; set; }
    }
}
