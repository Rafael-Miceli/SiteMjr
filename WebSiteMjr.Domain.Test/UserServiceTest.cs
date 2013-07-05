using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void Should_Return_User_Company()
        {
            var userRepository = new Mock<IUserRepository>();
            var companyRepository = new Mock<ICompanyRepository>();
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
            var fakeCompany = new Company
                {
                    Id = 1,
                    Name = "Rafael Company",
                    Email = "rafael.miceli@outlook.com"
                };
            userRepository.Setup(s => s.GetByUserName(fakeUser.Username)).Returns(fakeUser);
            companyRepository.Setup(s => s.GetById(fakeUser.IdCompany)).Returns(fakeCompany);
            var userService = new UserService(userRepository.Object, companyRepository.Object);

            var fakeCompanyReturned = userService.GetUserCompany(fakeUser.Username);

            Assert.IsTrue(fakeCompanyReturned == fakeCompany);
        }
    }
}
