using System;
using FlexProviders.Membership;
using FlexProviders.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers.Api;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.Domain.Test;

namespace WebSiteMjr.Tests.Integration.Controllers.Api
{
    [TestClass]
    public class EmployeeApiControllerTest
    {
        private IMembershipService _membershipService;
        private Mock<IApplicationEnvironment> _applicationEnvironmentMock;
        private Mock<IFlexMembershipRepository> _flexMembershipRepositoryMock;
        private Mock<IFlexRoleStore> _flexRoleStoreMock;
        private MjrAppRoleService _roleService;

        [TestInitialize]
        public void Initialize()
        {
            _flexMembershipRepositoryMock = new Mock<IFlexMembershipRepository>();
            _applicationEnvironmentMock = new Mock<IApplicationEnvironment>();

            

            _flexRoleStoreMock = new Mock<IFlexRoleStore>();

            _roleService = new MjrAppRoleService(new FlexRoleProvider(_flexRoleStoreMock.Object));

            _membershipService = new MembershipService(new FlexMembershipProvider(_flexMembershipRepositoryMock.Object, _applicationEnvironmentMock.Object), _roleService, new StubUnitOfWork());
        }


        [TestMethod]
        public void Should_Get_Employee_User()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();

            employeeServiceMock.Setup(x => x.FindEmployee(It.IsAny<object>())).Returns(new Employee
            {
                Id = 1
            });

            _flexMembershipRepositoryMock.Setup(x => x.GetUserByEmployeeId(1)).Returns(new User
            {
                Id = 1,
                Username = "Rafael"
            });

            var employeeApiController = new EmployeeApiController(employeeServiceMock.Object, _membershipService);

            var result = employeeApiController.GetEmployeeUser("1");

            Assert.IsNotNull(result);

        }
    }
}
