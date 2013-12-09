using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class CheckInToolServiceTest
    {
        [TestMethod]
        public void Should_Return_Checkin_By_Employee()
        {
            var employeeName = "Celso";
            var checkInToolService = new CheckInToolService();

            var checkinsByEmplyee = checkInToolService.GetCheckinsByEmployeeName(employeeName);

            Assert.AreEqual(null, checkinsByEmplyee.Where(c => c.Employee.Name != employeeName));
        }
    }
}
