using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Holder : IntId, IMjrException, INotDeletable
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public virtual string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IEnumerable<Tool> Tools { get; set; }

        public string ObjectName
        {
            get
            {
                return "Funcionário ou Condomínio/Empresa";
            }
        }
    }
}
