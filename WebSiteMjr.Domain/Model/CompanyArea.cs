using System.Collections.Generic;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CompanyArea: IntId, IMjrException
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Company> Companies { get; set; } 
        public string ObjectName
        {
            get
            {
                return "Área de condominio";
            }
        }
    }
}
