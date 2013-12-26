using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Controllers.Api;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Tests.Controllers.Api
{
    [TestClass]
    public class CheckinToolApiControllerTest
    {
        [TestMethod]
        public void Should_Get_EmployeeCompanyHolderNames()
        {
            var checkinToolApiController = new CheckinToolApiController(new StubCheckinToolService());

            var employeeCompanyHolderNames = checkinToolApiController.GetEmployeeCompanyHoldersName();

            Assert.AreNotEqual(0, employeeCompanyHolderNames.Count());
        }
    }

    public class StubCheckinToolService: ICheckinToolService
    {
        public void CheckinTool(CheckinTool checkinTool)
        {
            throw new NotImplementedException();
        }

        public void UpdateToolCheckin(CheckinTool checkinTool)
        {
            throw new NotImplementedException();
        }

        public void DeleteToolCheckin(object checkinTool)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckinTool> ListToolCheckins()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckinTool> FilterCheckins(Holder employeeName, string toolName, DateTime? date)
        {
            throw new NotImplementedException();
        }

        public CheckinTool FindToolCheckin(object idCheckinTool)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ListEmployeeCompanyHolderName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ListToolName()
        {
            throw new NotImplementedException();
        }

        public Holder FindEmployeeCompanyByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
