using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Address: Key<int>
    {
        public virtual string AddressName { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual AddressType AddressType { get; set; }
    }

    public enum AddressType
    {
        Home,
        Business
    }
}
