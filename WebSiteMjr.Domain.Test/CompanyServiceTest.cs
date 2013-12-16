using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class CompanyServiceTest
    {
        [TestMethod]
        public void Should_Return_Company_By_Id()
        {
            var companyRepository = new Mock<ICompanyRepository>();
            var fakeUser = new User
            {
                Id = 1,
                Name = "Rafael",
                LastName = "Paiva",
                Username = "rafael.miceli@outlook.com",
                Email = "rafael.miceli@outlook.com",
                IdCompany = 1,
                IsLocal = true,
                Password = "123",
                PasswordResetToken = "123",
                PasswordResetTokenExpiration = DateTime.Now,
                Roles = new List<Role>
                        {
                            new Role
                                {
                                    Id = 1,
                                    Name = "User",
                                }
                        }
            };
            var fakeCompanyId = fakeUser.IdCompany;
            companyRepository.Setup(s => s.GetById(fakeCompanyId)).Returns(() => new Company
            {
                Id = 1,
                Email = "rafael.miceli@outlook.com",
                Name = "Rafael Empresa"
            });
            //var companyService = new CompanyService(companyRepository.Object, new PersonsUow());

            //var fakeCompanyReturned = companyService.FindCompany(fakeCompanyId);

            //Assert.IsTrue(fakeCompanyReturned.Id == fakeCompanyId);
        }

        [TestMethod]
        public void Should_Return_Company_List()
        {
            var companyService = new CompanyService(new StubCompanyRepository(), null);

            var companies = companyService.ListCompany().ToList();

            Console.WriteLine(companies.Count());

            foreach (var company in companies)
            {
                Console.WriteLine(company.Name);
                Assert.AreNotEqual(null, company.Name);
            }
        }
    }

    public class StubCompanyRepository : ICompanyRepository
    {
        List<Company> _companies;

        public StubCompanyRepository()
        {
            CreateCompanies();
        }

        private void CreateCompanies()
        {
            _companies = new List<Company>()
            {
                new Company
                {
                    Name = "Company 1",
                    City = "Rio de Janeiro"
                },
                new Company
                {
                    Name = "Company 2",
                    City = "Rio de Janeiro"
                },
                new Company
                {
                    Name = "Company 3",
                    City = "Rio de Janeiro"
                },
            };
        }

        public void Add(Company entitie)
        {
            throw new NotImplementedException();
        }

        public void Remove(object entitie)
        {
            throw new NotImplementedException();
        }

        public void Update(Company entitie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetAll()
        {
            return _companies;
        }

        public IEnumerable<Company> Query(Func<Company, bool> filter)
        {
            throw new NotImplementedException();
        }

        public Company GetById(object identitie)
        {
            throw new NotImplementedException();
        }

        public Company Get(Expression<Func<Company, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Company GetCompanyByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
