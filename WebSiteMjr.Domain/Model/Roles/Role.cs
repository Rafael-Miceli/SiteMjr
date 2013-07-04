using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model.Roles
{
    public class Role : IntId, IFlexRole<User>
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
