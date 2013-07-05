using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void ShouldReturnUserCompany()
        {
            //Create fake user
            var fakeUser = new User
                {
                    Id = 1,
                    Name = "Rafael",
                    LastName = "Paiva",
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
            var fakeCompanyService = new CompanyService(new CompanyRepository());
            var fakeCompanyId = fakeUser.IdCompany;

            var fakeCompanyReturned = fakeCompanyService.FindCompany(fakeCompanyId);

            Assert.IsTrue(fakeCompanyReturned.Id == fakeCompanyId);
        }
    }
}
