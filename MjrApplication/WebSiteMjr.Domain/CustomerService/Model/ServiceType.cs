using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class ServiceType: Key<int>
    {
        public virtual string Details { get; set; }
    }
}