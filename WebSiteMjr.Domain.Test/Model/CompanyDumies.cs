using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CompanyDumies
    {
        public static Company CreateOneCompany()
        {
            return new Company
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "2455-3100"
            };
        }

        public static List<Company> CreateListOfCompanies()
        {
            return new List<Company>
            {
                new Company
                {
                    Name = "Portoverano",
                    Address = "Rua gastao senges",
                    City = "Rio de Janeiro",
                    Email = "adm@portoverano.com",
                    Id = 2,
                    Phone = "2455-3100"
                },
                new Company
                {
                    Name = "Portomare",
                    Address = "Rua gastao senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Portomare.com",
                    Id = 3,
                    Phone = "2455-3101"
                },
                new Company
                {
                    Name = "Portofelice",
                    Address = "Rua gastao senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Portofelice.com",
                    Id = 4,
                    Phone = "2455-3102"
                },
                new Company
                {
                    Name = "Borbouns",
                    Address = "Rua Maria senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Borbouns.com",
                    Id = 5,
                    Phone = "2455-3103"
                }
            };
        }
    }
}