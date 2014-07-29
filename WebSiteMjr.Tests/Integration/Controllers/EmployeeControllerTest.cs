using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlexProviders.Membership;
using FlexProviders.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.Domain.Test;
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
        private IMembershipService _membershipService;
        private Mock<IApplicationEnvironment> _applicationEnvironmentMock;
        private Mock<IFlexMembershipRepository> _flexMembershipRepositoryMock;
        private Mock<IFlexRoleStore> _flexRoleStoreMock;
        private MjrAppRoleService _roleService;
        private Mock<IEmailService> _emailServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cacheServiceMock = new Mock<ICacheService>();
            _membershipServiceMock = new Mock<IMembershipService>();

            _flexMembershipRepositoryMock = new Mock<IFlexMembershipRepository>();
            _applicationEnvironmentMock = new Mock<IApplicationEnvironment>();

            _flexRoleStoreMock = new Mock<IFlexRoleStore>();

            _emailServiceMock = new Mock<IEmailService>();

            _roleService = new MjrAppRoleService(new FlexRoleProvider(_flexRoleStoreMock.Object));
            _membershipService = new MembershipService(new FlexMembershipProvider(_flexMembershipRepositoryMock.Object, _applicationEnvironmentMock.Object, new StubUnitOfWork()), _roleService);
            _employeeController = new EmployeeController(new EmployeeService(_employeeRepositoryMock.Object, _membershipService, _emailServiceMock.Object, _unitOfWorkMock.Object), _cacheServiceMock.Object, _membershipServiceMock.Object);
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Employee_With_Login_Should_Create_Both_In_Database_And_Send_An_Email_To_Created_Employee()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                GenerateLogin = true
            };

            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());
            _flexRoleStoreMock.Setup(x => x.GetAllRoles()).Returns(RoleDummies.ReturnAllRoles());
            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), createEmployeeViewModel.Email, createEmployeeViewModel.Name, createEmployeeViewModel.LastName));
            _employeeRepositoryMock.Setup(x => x.Add(It.IsAny<Employee>()));
            _flexMembershipRepositoryMock.Setup(x => x.Add(It.IsAny<User>()));

            var result = _employeeController.Create(createEmployeeViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            
            _cacheServiceMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
            _emailServiceMock.VerifyAll();
            _employeeRepositoryMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_An_Employee_With_Same_Email_Data_Of_Another_When_Creating_Employee_With_Login_Should_Return_Message_Error()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                GenerateLogin = true
            };

            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());
            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), createEmployeeViewModel.Email, createEmployeeViewModel.Name, createEmployeeViewModel.LastName));
            _employeeRepositoryMock.Setup(x => x.GetEmployeeByEmail(createEmployeeViewModel.Email)).Returns(EmployeeDummies.CreateListOfEmployees().First());

            var result = _employeeController.Create(createEmployeeViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual("Este E-mail já existe para outro funcionário", _employeeController.ModelState["EmailExists"].Errors[0].ErrorMessage);

            _cacheServiceMock.VerifyAll();
            _employeeRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Login_For_Existent_Employee_Then_Should_Create_New_User_For_Employee_And_Send_An_Email_To_Created_Employee()
        {
            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com"
            };

            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());
            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), employee.Email, employee.Name, employee.LastName));
            _employeeRepositoryMock.Setup(x => x.GetEmployeeByEmail(employee.Email)).Returns((Employee) null);

            var result = _employeeController.CreateLoginForExistentEmployee(employee) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            _cacheServiceMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
            _emailServiceMock.VerifyAll();
            _employeeRepositoryMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_A_Request_To_List_All_Active_Users_From_The_Company_Who_Is_Requesting_When_Requesting_Then_Should_Return_All_Active_Users_From_That_Company()
        {
            //TODO Need to create unit test to return of employees from on company

            _cacheServiceMock.Setup(x => x.Get("User", It.IsAny<Func<User>>())).Returns(UserDummies.ReturnOneMjrActiveUser());
            _employeeRepositoryMock.Setup(x => x.GetAll()).Returns(EmployeeDummies.CreateListOfEmployees());

            var result = _employeeController.Index() as ViewResult;


            Assert.IsNotNull(result);
            Assert.IsFalse(((IEnumerable<Employee>)result.Model).Any(em => em.Company.Id != UserDummies.ReturnOneMjrActiveUser().Employee.Company.Id));
        }
    }

    //public class FakeEmployeeRepository
}
