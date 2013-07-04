using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Phone: IntId
    {
        public virtual string PhoneNumber { get; set; }
    }
}
