using System.Collections.Generic;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Employee: IntId, IHolder
    {
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string LastName { get; set; }
        public virtual int IdUser { get; set; }
        public virtual IEnumerable<Stuff> Stuff { get; set; }
    }
}
