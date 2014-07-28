using System;
using System.Web.Mvc;
using FlexProviders.Membership;
using FlexProviders.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;
using WebSiteMjr.Models;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Tests.Integration_To_Data_Layer.Controllers
{
    [TestClass]
    public class EmployeeControllerIntegrationToDataTest
    {
        private Mock<ICacheService> _cacheServiceMock;
        private EmployeeController _employeeController;
        private Mock<IMembershipService> _membershipServiceMock;
        private IMembershipService _membershipService;
        private Mock<IApplicationEnvironment> _applicationEnvironmentMock;
        private Mock<IFlexRoleStore> _flexRoleStoreMock;
        private MjrAppRoleService _roleService;

        [TestInitialize]
        [Ignore]
        public void Initialize()
        {
            _cacheServiceMock = new Mock<ICacheService>();
            _membershipServiceMock = new Mock<IMembershipService>();

            _applicationEnvironmentMock = new Mock<IApplicationEnvironment>();

            _flexRoleStoreMock = new Mock<IFlexRoleStore>();

            var personUoW = new PersonsUow(new PersonsContext());

            _roleService = new MjrAppRoleService(new FlexRoleProvider(new RoleRepository<MjrAppRole, User>(personUoW)));
            _membershipService = new MembershipService(new FlexMembershipProvider(new MembershipRepository(personUoW), _applicationEnvironmentMock.Object, personUoW), _roleService);
            _employeeController = new EmployeeController(new EmployeeService(new EmployeeRepository(personUoW), _membershipService, personUoW), _cacheServiceMock.Object, _membershipService);
        }

        [TestMethod]
        [Ignore]
        public void Given_A_Valid_Employee_Data_When_Creting_Employee_With_Login_Should_Create_Both_In_Database_And_Send_An_Email_To_Created_Employee()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                Phone = "(21) 98802-3931",
                GenerateLogin = true
            };

            _membershipServiceMock.Setup(x => x.GetLoggedUser("teste")).Returns(UserDummies.ReturnOneMjrActiveUser());
            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());

            _flexRoleStoreMock.Setup(x => x.GetAllRoles()).Returns(RoleDummies.ReturnAllRoles());

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
