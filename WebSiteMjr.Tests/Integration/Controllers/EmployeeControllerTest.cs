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
using WebSiteMjr.Facade;
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

            _flexMembershipRepositoryMock = new Mock<IFlexMembershipRepository>();
            _applicationEnvironmentMock = new Mock<IApplicationEnvironment>();

            _flexRoleStoreMock = new Mock<IFlexRoleStore>();

            _emailServiceMock = new Mock<IEmailService>();

            _roleService = new MjrAppRoleService(new FlexRoleProvider(_flexRoleStoreMock.Object));
            _membershipService = new MembershipService(new FlexMembershipProvider(_flexMembershipRepositoryMock.Object, _applicationEnvironmentMock.Object), _roleService, new StubUnitOfWork());
            _employeeController = new EmployeeController(
                new EmployeeLoginFacade(
                    new EmployeeService(_employeeRepositoryMock.Object, _unitOfWorkMock.Object)
                    , _membershipService, _emailServiceMock.Object, _unitOfWorkMock.Object),
                    _cacheServiceMock.Object);
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Employee_With_Login_Option_Ture_Should_Create_Both_In_Database_And_Send_An_Email_To_Created_Employee()
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
            _flexMembershipRepositoryMock.Setup(x => x.GetUserByUsername(createEmployeeViewModel.Email)).Returns(UserDummies.ReturnOneMjrActiveUser);

            var result = _employeeController.Create(createEmployeeViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual("Este E-mail já existe para outro funcionário", _employeeController.ModelState["EmailExists"].Errors[0].ErrorMessage);

            _cacheServiceMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Login_For_Existent_Employee_Then_Should_Create_New_User_For_Employee_And_Send_An_Email_To_Updated_Employee()
        {

            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                Company = CompanyDummies.CreateMjrCompany()
            };
            
            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), employee.Email, employee.Name, employee.LastName));
            _employeeRepositoryMock.Setup((x => x.GetById(It.IsAny<object>()))).Returns(employee);
            _flexMembershipRepositoryMock.Setup(x => x.GetUserByUsername(employee.Email)).Returns((User) null);
            _flexMembershipRepositoryMock.Setup(x => x.Add(It.IsAny<User>()));

            var result = _employeeController.CreateLoginForExistentEmployee(employee) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            _employeeRepositoryMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
            _emailServiceMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Login_For_Existent_Employee_That_Had_No_Email_Then_Should_Update_Employee_Email_And_Create_New_User_For_Employee_And_Send_An_Email_To_Updated_Employee()
        {

            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                Company = CompanyDummies.CreateMjrCompany()
            };

            var employeeInDb = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Company = CompanyDummies.CreateMjrCompany()
            };

            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), employee.Email, employee.Name, employee.LastName));
            _employeeRepositoryMock.Setup((x => x.GetById(It.IsAny<object>()))).Returns(employeeInDb);
            _flexMembershipRepositoryMock.Setup(x => x.GetUserByUsername(employee.Email)).Returns((User)null);
            _flexMembershipRepositoryMock.Setup(x => x.Add(It.IsAny<User>()));

            var result = _employeeController.CreateLoginForExistentEmployee(employee) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            AssertUpdatedEmployeeFields(employee, employeeInDb);

            _employeeRepositoryMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
            _emailServiceMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void Given_A_Valid_Employee_Data_When_Creating_Login_For_Existent_Employee_Then_Should_Update_Employee_Datas_And_Create_New_User_For_Employee_And_Send_An_Email_To_Updated_Employee()
        {

            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Miceli",
                Email = "rafael.miceli@hotmail.com",
                Phone = "(21) 98802-3922"
            };

            var employeeInDb = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Company = CompanyDummies.CreateMjrCompany()
            };

            _emailServiceMock.Setup(x => x.SendFirstLoginToEmployee(It.IsAny<string>(), employee.Email, employee.Name, employee.LastName));
            _employeeRepositoryMock.Setup((x => x.GetById(It.IsAny<object>()))).Returns(employeeInDb);
            _flexMembershipRepositoryMock.Setup(x => x.GetUserByUsername(employee.Email)).Returns((User)null);
            _flexMembershipRepositoryMock.Setup(x => x.Add(It.IsAny<User>()));

            var result = _employeeController.CreateLoginForExistentEmployee(employee) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            AssertUpdatedEmployeeFields(employee, employeeInDb);

            _employeeRepositoryMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
            _emailServiceMock.VerifyAll();
            _flexMembershipRepositoryMock.VerifyAll();
        }

        private void AssertUpdatedEmployeeFields(Employee employee, Employee employeeInDb)
        {
            Assert.AreEqual(employee.Phone, employeeInDb.Phone);
            Assert.AreEqual(employee.Name, employeeInDb.Name);
            Assert.AreEqual(employee.LastName, employeeInDb.LastName);
            Assert.AreEqual(employee.Email, employeeInDb.Email);
        }

        [TestMethod]
        public void Given_A_Employee_Data_Without_Email_When_Creating_Login_For_Existent_Employee_Then_Should_Not_Create_New_User_For_Employee_And_Return_Error_Message()
        {
            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Company = CompanyDummies.CreateMjrCompany()
            };

            var result = _employeeController.CreateLoginForExistentEmployee(employee);

            Assert.IsNotNull(result);
            Assert.AreEqual("Para criar um login para o funcionário, preencha o E-mail do mesmo.", _employeeController.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Given_A_Employee_Data_With_Email_Existent_When_Creating_Login_For_Existent_Employee_Then_Should_Not_Create_New_User_For_Employee_And_Return_Error_Message()
        {
            var employee = new Employee
            {
                Name = "Quezia",
                LastName = "Mello",
                Email = "rafael.miceli@hotmail.com",
                Company = CompanyDummies.CreateMjrCompany()
            };

            _employeeRepositoryMock.Setup((x => x.GetById(It.IsAny<object>()))).Returns(employee);
            _flexMembershipRepositoryMock.Setup(x => x.GetUserByUsername(employee.Email)).Returns(UserDummies.ReturnOneMjrActiveUser);

            var result = _employeeController.CreateLoginForExistentEmployee(employee);

            Assert.IsNotNull(result);
            Assert.AreEqual("Este E-mail já existe para outro funcionário", _employeeController.ModelState["EmailExists"].Errors[0].ErrorMessage);

            _employeeRepositoryMock.VerifyAll();
            _flexRoleStoreMock.VerifyAll();
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

        [TestMethod]
        public void Given_A_Request_To_Delete_An_Employee_When_Deleting_Employee_Then_Delete_Inactive_Employee_And_Inactive_User_Associated_To_Employee()
        {
            var user = UserDummies.ReturnOneMjrActiveUser();

            _flexMembershipRepositoryMock.Setup(x => x.GetActiveUserByEmployeeId(It.IsAny<int>())).Returns(user);
            _flexMembershipRepositoryMock.Setup(x => x.Save(It.IsAny<User>()));

            var result = _employeeController.DeleteConfirmed(EmployeeDummies.CreateListOfEmployees().First()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            _flexMembershipRepositoryMock.VerifyAll();
        }

    }
}
