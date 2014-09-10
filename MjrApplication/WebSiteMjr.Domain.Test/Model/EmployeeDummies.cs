using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public class EmployeeDummies
    {
        public static IEnumerable<Employee> CreateListOfEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Celso",
                    LastName = "Gay",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreateMjrCompany()
                },
                new Employee
                {
                    Id = 2,
                    Name = "Brendon",
                    LastName = "Gay",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreateMjrCompany()
                },
                new Employee
                {
                    Id = 3,
                    Name = "Lorena",
                    LastName = "Gay",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreateMjrCompany()
                },
                new Employee
                {
                    Id = 4,
                    Name = "Gabriela",
                    LastName = "Gay",
                    IsDeleted = true,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreateMjrCompany()
                },
                new Employee
                {
                    Id = 5,
                    Name = "Maria",
                    LastName = "Hetero",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreatePortofinoCompany()
                },
                new Employee
                {
                    Id = 6,
                    Name = "José",
                    LastName = "Hetero",
                    IsDeleted = true,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreatePortofinoCompany()
                },
                new Employee
                {
                    Id = 6,
                    Name = "Valdir",
                    LastName = "Hetero",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreatePortofinoCompany()
                },
                new Employee
                {
                    Id = 7,
                    Name = "Administrator",
                    LastName = "Hetero",
                    IsDeleted = false,
                    Email = "rafael.miceli@hotmail.com",
                    Company = CompanyDummies.CreateMjrCompany()
                }
            };
        }
    }
}