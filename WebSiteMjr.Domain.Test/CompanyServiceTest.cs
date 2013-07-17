using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

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
    }
}
