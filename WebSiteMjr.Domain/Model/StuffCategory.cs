using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffCategory : Key<int>, IMjrException, IObjectWithState
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
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