using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Company : Holder
    {
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "E-mail inválido")]
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$",
                           ErrorMessage = "Telefone Inválido")]
        public virtual string Phone { get; set; }
    }
}
