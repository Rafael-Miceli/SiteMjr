using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Company: IntId
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}
