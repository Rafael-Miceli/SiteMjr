using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffCategory: IntId, IMjrException
    {
        public string Name { get; set; }
        public string ObjectName {
            get
            {
                return "Categoria de Material";    
            } }
    }
}