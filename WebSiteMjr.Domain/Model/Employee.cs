using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class Employee: IntId
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LastName { get; set; }
        public int IdUser { get; set; }
    }
}
