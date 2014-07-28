﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FlexProviders.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.Test;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.Models;

namespace WebSiteMjr.Tests.Integration.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private IMembershipService _membershipService;
        private Mock<IFlexMembershipRepository> _flexMembershipRepository;
        private Mock<IApplicationEnvironment> _applicationEnvironment;
        private Mock<ICacheService> _cacheServiceMock;
        private UrlHelper _urlHelperMock;
        private AccountController _accountController;

        [TestInitialize]
        public void Initialize()
        {
            _flexMembershipRepository = new Mock<IFlexMembershipRepository>();
            _applicationEnvironment = new Mock<IApplicationEnvironment>();
            _cacheServiceMock = new Mock<ICacheService>();

            var httpCtxStub = new Mock<HttpContextBase>();
            var requestContextMock = new RequestContext { HttpContext = httpCtxStub.Object };
            _urlHelperMock = new UrlHelper(requestContextMock);

            _membershipService =
                new MembershipService(new FlexMembershipProvider(_flexMembershipRepository.Object, _applicationEnvironment.Object, new StubUnitOfWork()), null);

            _accountController = new AccountController(_membershipService, _cacheServiceMock.Object) { Url = _urlHelperMock };
        }

        [TestMethod]
        public void Given_A_Valid_Login_With_No_ReturnUrl_When_Login_Then_User_Should_Be_LogedIn_And_Redirected_To_Home()
        {
            _flexMembershipRepository.Setup(x => x.GetUserByUsername("Administrator"))
                .Returns(UserDummies.ReturnOneMjrActiveUser);

            var loginModel = new LoginModel
            {
                UserName = "Administrator",
                Password = "12345678",
                RememberMe = false
            };

            var result = _accountController.Login(loginModel, "") as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Given_An_Invalid_UserName_When_Login_Then_User_Should_Not_Be_LogedIn_And_Return_Error_Message()
        {
            _flexMembershipRepository.Setup(x => x.GetUserByUsername("Administrator"))
                .Returns(UserDummies.ReturnOneMjrActiveUser);

            var loginModel = new LoginModel
            {
                UserName = "Administrato",
                Password = "12345678",
                RememberMe = false
            };

            var model = (_accountController.Login(loginModel, "") as ViewResult).Model;

            Assert.IsNotNull(model);
            Assert.AreEqual(loginModel.UserName, ((LoginModel)model).UserName);
            Assert.AreEqual("O nome de usuário ou senha estão incorretos.", _accountController.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Given_A_Invalid_Password_When_Login_Then_User_Should_Not_Be_LogedIn_And_Return_Error_Message()
        {
            _flexMembershipRepository.Setup(x => x.GetUserByUsername("Administrator"))
                .Returns(UserDummies.ReturnOneMjrActiveUser);

            var loginModel = new LoginModel
            {
                UserName = "Administrator",
                Password = "123456789",
                RememberMe = false
            };

            var model = (_accountController.Login(loginModel, "") as ViewResult).Model;

            Assert.IsNotNull(model);
            Assert.AreEqual(loginModel.Password, ((LoginModel)model).Password);
            Assert.AreEqual("O nome de usuário ou senha estão incorretos.", _accountController.ModelState[""].Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Given_A_Valid_RegisterModel_Of_A_New_User_When_Register_User_Should_Create_In_Db_And_Redirected_To_Home()
        {
            _flexMembershipRepository.Setup(x => x.GetUserByUsername("Administrator"))
                .Returns(UserDummies.ReturnOneMjrActiveUser);

            var registerModel = new RegisterModel
            {
                UserName = "Administrador@portoverano.com",
                Password = "123456789"
            };

            var result = _accountController.Register(registerModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Given_An_Invalid_RegisterModel_Of_A_New_User_When_Register_User_Should_Create_In_Db_And_Redirected_To_Home()
        {
            _flexMembershipRepository.Setup(x => x.GetUserByUsername("Administrator"))
                .Returns(UserDummies.ReturnOneMjrActiveUser);

            var registerModel = new RegisterModel
            {
                UserName = "Administrator",
                Password = "123456789"
            };

            var model = (_accountController.Register(registerModel) as ViewResult).Model;

            Assert.IsNotNull(model);
            Assert.AreEqual(registerModel.UserName, ((RegisterModel)model).UserName);
            Assert.AreEqual("Usuário já existente. por favor entre com um usuário diferente.", _accountController.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}
