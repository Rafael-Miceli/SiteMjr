using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffManufacture : Key<int>, IMjrException, IObjectWithState
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Name { get; set; }

        public string ObjectName
        {
            get
            {
                return "Fabricante do material";
            }
        }

        [NotMapped]
        public State State { get; set; }
    }
}