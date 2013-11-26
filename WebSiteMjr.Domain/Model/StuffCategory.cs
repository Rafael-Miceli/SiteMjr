using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffCategory: IntId, IMjrException, IObjectWithState
    {
        public string Name { get; set; }
        public string ObjectName {
            get
            {
                return "Categoria de Material";    
            } }

        [NotMapped]
        public State State { get; set; }
    }
}