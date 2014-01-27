using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfStuffData.Context;
using WebSiteMjr.EfStuffData.DataRepository;
using WebSiteMjr.EfStuffData.UnitOfWork;

namespace WebSiteMjr.EfStuffDataTest.IntegrationTest
{
    [TestClass]
    public class CheckinToolRepositoryTest
    {
        [TestMethod]
        public void Should_Create_CheckinTool_In_The_Database()
        {
            //Arrange
            var stuffContext = new StuffContext();
            var personContext = new PersonsContext();
            var stuffUow = new StuffUow(stuffContext);

            var checkinToolRepository = new CheckinToolRepository(stuffUow);

            var holderId = 4;
            var tool = stuffContext.Tools.Find(1);
            var companyArea = personContext.CompanyAreas.Find(6);

            var newCheckin = new CheckinTool
            {
                Tool = tool,
                EmployeeCompanyHolderId = holderId,
                CheckinDateTime = DateTime.Now,
                CompanyAreaId = companyArea.Id
            };

            //Act
            checkinToolRepository.Add(newCheckin);

            //Assert
        }
    }
}
