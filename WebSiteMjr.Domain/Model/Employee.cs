using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Employee: IntId, IHolder
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Name { get; set; }
        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{5}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$", 
            ErrorMessage = "Telefone Inválido")]
        [MaskPropertie(MaskType = "CelPhone")]
        public virtual string Phone { get; set; }
        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        public virtual string LastName { get; set; }
        public virtual int IdUser { get; set; }
        public IEnumerable<Stuff> Stuff { get; set; }
    }
}
