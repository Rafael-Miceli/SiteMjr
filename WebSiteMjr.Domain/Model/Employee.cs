using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Interfaces.Model;

namespace WebSiteMjr.Domain.Model
{
    public class Employee: Holder
    {
        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{5}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$", 
            ErrorMessage = "Telefone Inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) #####-####}")]
        public virtual string Phone { get; set; }
        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        public virtual string LastName { get; set; }
        public virtual int IdUser { get; set; }
    }
}
