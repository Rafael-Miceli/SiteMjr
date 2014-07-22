using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mjr.Extensions;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void Should_Return_User_Company()
        {
            //var userRepository = new Mock<IUserRepository>();
            //var companyRepository = new Mock<ICompanyRepository>();
            //var fakeUser = new User
            //    {
            //        Id = 1,
            //        Username = "rafael.miceli@outlook.com",
            //        IsLocal = true,
            //        Password = "123",
            //        PasswordResetToken = "123",
            //        PasswordResetTokenExpiration = DateTime.UtcNow.ConvertToTimeZone(),
            //        Roles = new List<Role>
            //            {
            //                new Role
            //                    {
            //                        Id = 1,
            //                        Name = "User",
            //                    }
            //            }
            //    };
            //var fakeCompany = new Company
            //    {
            //        Id = 1,
            //        Name = "Rafael Company",
            //        Email = "rafael.miceli@outlook.com"
            //    };
            //userRepository.Setup(s => s.GetByUserName(fakeUser.Username)).Returns(fakeUser);
            ////companyRepository.Setup(s => s.GetById(fakeUser.IdCompany)).Returns(fakeCompany);
            ////var userService = new UserService(userRepository.Object, companyRepository.Object, new PersonsUow());

            //var fakeCompanyReturned = userService.GetUserCompany(fakeUser.Username);

            //Assert.IsTrue(fakeCompanyReturned == fakeCompany);
        }

        [TestMethod]
        public void Should_Not_Return_User_Company()
        {
            //var userRepository = new Mock<IUserRepository>();
            //var companyRepository = new Mock<ICompanyRepository>();
            //var fakeUser = new User
            //{
            //    Id = 1,
            //    Name = "Rafael",
            //    LastName = "Paiva",
            //    Username = "rafael.miceli@outlook.com",
            //    Email = "rafael.miceli@outlook.com",
            //    IdCompany = 1,
            //    IsLocal = true,
            //    Password = "123",
            //    PasswordResetToken = "123",
            //    PasswordResetTokenExpiration = DateTime.UtcNow.ConvertToTimeZone(),
            //    Roles = new List<Role>
            //            {
            //                new Role
            //                    {
            //                        Id = 1,
            //                        Name = "User",
            //                    }
            //            }
            //};
            //var fakeCompany = new Company
            //{
            //    Id = 4,
            //    Name = "Rafael Company",
            //    Email = "rafael.miceli@outlook.com"
            //};
            //userRepository.Setup(s => s.GetByUserName(fakeUser.Username)).Returns(fakeUser);
            //companyRepository.Setup(s => s.GetById(4)).Returns(fakeCompany);
            //var userService = new UserService(userRepository.Object, companyRepository.Object, new PersonsUow());

            //var fakeCompanyReturned = userService.GetUserCompany(fakeUser.Username);

            //Assert.IsNull(fakeCompanyReturned);
        }
    }
}
