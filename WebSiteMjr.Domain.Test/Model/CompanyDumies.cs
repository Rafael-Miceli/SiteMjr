using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels.Company;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CompanyDummies
    {
        public static Company CreatePortofinoCompany()
        {
            return new Company
            {
                Name = "Portofino",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 6,
                Phone = "(21) 2455-3100"
            };
        }

        public static Company CreateMjrCompany()
        {
            return new Company
            {
                Name = "Mjr",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "mjr@mjr.com.br",
                Id = 1,
                Phone = "(21) 2455-3100"
            };
        }

        public static EditCompanyViewModel CreateOneEditCompanyViewModel()
        {
            return new EditCompanyViewModel
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "(21) 2455-3100",
                CompanyAreas = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Selected = false,
                        Text = "Portão de visitantes",
                        Value = "2"
                    }
                }
            };
        }

        public static EditCompanyViewModel CreateOneEditCompanyViewModelWithCompanyArea()
        {
            return new EditCompanyViewModel
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "(21) 2455-3100",
                CompanyAreas = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Selected = true,
                        Text = "Portão de visitantes",
                        Value = "2"
                    }
                }
            };
        }

        public static EditCompanyViewModel CreateOneEditCompanyViewModelWithNoCompanyArea()
        {
            return new EditCompanyViewModel
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "(21) 2455-3100"
            };
        }

        public static Company CreateOneCompanyWithCompanyArea()
        {
            return new Company
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "(21) 2455-3100",
                CompanyAreas = new Collection<CompanyArea>
                {
                    CompanyAreasDummies.CreateListOfCompanyAreas().Find(t => t.Id == 2)
                }
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
                    Phone = "(21) 2455-3100",
                    IsDeleted = false
                },
                new Company
                {
                    Name = "Portomare",
                    Address = "Rua gastao senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Portomare.com",
                    Id = 3,
                    Phone = "(21) 2455-3101",
                    IsDeleted = false
                },
                new Company
                {
                    Name = "Portofelice",
                    Address = "Rua gastao senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Portofelice.com",
                    Id = 4,
                    Phone = "(21) 2455-3102",
                    IsDeleted = false
                },
                new Company
                {
                    Name = "Borbouns",
                    Address = "Rua Maria senges",
                    City = "Rio de Janeiro",
                    Email = "adm@Borbouns.com",
                    Id = 5,
                    Phone = "(21) 2455-3103",
                    IsDeleted = true
                }
            };
        }
    }
}