using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Phone : Key<int>
    {
        public virtual string PhoneNumber { get; set; }
    }
}
