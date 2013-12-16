using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Controllers.Api;

namespace WebSiteMjr.Tests.Controllers.Api
{
    [TestClass]
    public class CheckinToolApiControllerTest
    {
        [TestMethod]
        public void Should_Get_EmployeeCompanyHolderNames()
        {
            var checkinToolApiController = new CheckinToolApiController();

            var employeeCompanyHolderNames = checkinToolApiController.GetEmployeeCompanyHoldersName();

            Assert.AreNotEqual(0, employeeCompanyHolderNames.Count());
        }
    }
}
