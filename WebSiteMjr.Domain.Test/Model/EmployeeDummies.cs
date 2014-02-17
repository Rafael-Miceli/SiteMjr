using System.Collections.Generic;
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

        public static IEnumerable<Employee> CreateListOfEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Celso",
                    LastName = "Gay",
                    IsDeleted = false
                },
                new Employee
                {
                    Id = 2,
                    Name = "Brendon",
                    LastName = "Gay",
                    IsDeleted = false
                },
                new Employee
                {
                    Id = 3,
                    Name = "Lorena",
                    LastName = "Gay",
                    IsDeleted = false
                },
                new Employee
                {
                    Id = 4,
                    Name = "Gabriela",
                    LastName = "Gay",
                    IsDeleted = true
                }
            };
        }
    }
}