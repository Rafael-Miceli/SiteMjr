using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffManufacture: IntId, IMjrException
    {
        public string Name { get; set; }

        public string ObjectName
        {
            get
            {
                return "Fabricante do material";
            }
        }
    }
}