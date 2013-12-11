using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Employee: Holder
    {
        [RegularExpression(@"^(\([0-9][0-9]\) [0-9][0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$", ErrorMessage = "Telefone Inválido")]
        public virtual string Phone { get; set; }
        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        public virtual string LastName { get; set; }
        public virtual int IdUser { get; set; }
    }
}
