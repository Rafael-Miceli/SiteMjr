using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class CheckInToolServiceTest
    {
        [TestMethod]
        public void Should_Return_Checkin_By_Employee()
        {
            var employeeName = "Celso";
            var checkInToolService = new CheckinToolService();

            var checkinsByEmplyee = checkInToolService.GetCheckinsByEmployeeName(employeeName);

            Assert.AreEqual(0, checkinsByEmplyee.Count(c => c.Employee.Name != employeeName));
        }

        public void Should_Return_Checkin_By_Employee_And_by_Tool()
        {
            var employeeName = "Celso";
            var checkInToolService = new CheckinToolService();

            var checkinsByEmplyee = checkInToolService.GetCheckinsByEmployeeName(employeeName);

            Assert.AreEqual(0, checkinsByEmplyee.Count(c => c.Employee.Name != employeeName));
        }
    }
}
