using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public abstract class ServiceType: Key<int>
    {
        public abstract string Details { get; set; }
    }
}