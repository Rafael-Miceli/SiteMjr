using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public virtual string Name { get; set; }
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "E-mail inválido")]
        public virtual string Email { get; set; }

        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{5}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$",
            ErrorMessage = "Telefone Inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) #####-####}")]
        public virtual string Phone { get; set; }
        [Required(ErrorMessage = "Sobrenome é um campo obrigatório")]
        public virtual string LastName { get; set; }

        public bool GenerateLogin { get; set; }
    }
}
