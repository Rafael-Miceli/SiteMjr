using System.Data.Entity;

namespace WebSiteMjr.Models
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext()
            :base("DefaultConnection")
        {
            
        }
    }

    public class Employee
    {
    }
}