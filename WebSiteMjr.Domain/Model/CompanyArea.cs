using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CompanyArea: Key<int>, IMjrException
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Company> Companies { get; set; } 

        [NotMapped]
        public string ObjectName
        {
            get
            {
                return "Área de condominio";
            }
        }

        [NotMapped]
        public virtual bool IsNull { get { return false; }}
    }
}
