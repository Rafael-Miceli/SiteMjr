using System.Collections.Generic;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Company: IntId, IHolder
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone { get; set; }
        public virtual IEnumerable<Stuff> Stuff { get; set; }
    }
}
