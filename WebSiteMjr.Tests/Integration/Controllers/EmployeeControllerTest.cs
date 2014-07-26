using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.Models;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Tests.Integration.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICacheService> _cacheServiceMock;
        private EmployeeController _employeeController;
        private Mock<IMembershipService> _membershipServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cacheServiceMock = new Mock<ICacheService>();
            _membershipServiceMock = new Mock<IMembershipService>();

            _employeeController = new EmployeeController(new EmployeeService(_employeeRepositoryMock.Object, _unitOfWorkMock.Object), _cacheServiceMock.Object, _membershipServiceMock.Object);
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creting_Employee_With_Login_Should_Create_Both_In_Database_And_Send_An_Email_To_Created_Employee()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                GenerateLogin = true
            };

            _membershipServiceMock.Setup(x => x.GetLoggedUser("teste")).Returns(UserDummies.ReturnOneMjrActiveUser());
            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());

            var result = _employeeController.Create(createEmployeeViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Given_A_Request_To_List_All_Active_Users_From_The_Company_Who_Is_Requesting_When_Requesting_Then_Should_Return_All_Active_Users_From_That_Company()
        {
            //TODO Need to create unit test to return of employees from on company

            //_cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>)).Returns((Func<User>) UserDummies.ReturnOneMjrActiveUser);

            //var result = _employeeController.Index();

            //Assert.IsNotNull(result);
        }
    }
}
