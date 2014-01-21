using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public class EmployeeDummies
    {
        public static Employee CreateOneEmployee()
        {
            return new Employee
            {
                Id = 2,
                Name = "Celso"
            };
        }
    }
}