using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class ToolLocalization: IntId, IMjrException
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Company> Companies { get; set; } 
        public string ObjectName
        {
            get
            {
                return "Localização de Ferramenta";
            }
        }
    }
}
