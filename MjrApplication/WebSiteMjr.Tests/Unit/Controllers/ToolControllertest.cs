using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Tests.Controllers
{
    [TestClass]
    public class ToolControllerTest
    {
        [TestMethod]
        public void Should_Load_CehckinTab_With_CheckinTools()
        {
            //Arrange
            var toolId = 1;

            var checkinToolDummies = CheckinToolDummies.CreateCheckinTools().Where(t => t.Tool.Id == toolId);
            var companyAreaDummies = CompanyAreasDummies.CreateListOfCompanyAreas();
            var holderDummies = CheckinToolDummies.CreateHoldersForCheckins();

            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            checkinToolServiceMock.Setup(x => x.ListCheckinToolsWithActualTool(toolId)).Returns(checkinToolDummies);

            var companyAreaServiceMock = new Mock<ICompanyAreasService>();
            companyAreaServiceMock.Setup(x => x.ListCompanyAreas()).Returns(companyAreaDummies);

            var holderServiceMock = new Mock<IHolderService>();
            holderServiceMock.Setup(x => x.ListHolder()).Returns(holderDummies);

            var toolController = new ToolController(null, null, null, holderServiceMock.Object,
                companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var model = (toolController.CheckinTab(toolId) as ViewResult).Model;
            //Assert
            Assert.IsNotNull(model);
            Assert.IsNotNull(((CheckinToolTabViewModel)model).CheckinTools);
            Assert.IsTrue(((CheckinToolTabViewModel)model).CheckinTools.Any());
        }

        [TestMethod]
        public void Should_Load_CheckinTab_With_DateTime_To_Checkin_Equal_Today()
        {
            //Arrange
            var toolId = 1;

            var checkinToolDummies = CheckinToolDummies.CreateCheckinTools().Where(t => t.Tool.Id == toolId);
            var companyAreaDummies = CompanyAreasDummies.CreateListOfCompanyAreas();
            var holderDummies = CheckinToolDummies.CreateHoldersForCheckins();

            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            checkinToolServiceMock.Setup(x => x.ListCheckinToolsWithActualTool(toolId)).Returns(checkinToolDummies);

            var companyAreaServiceMock = new Mock<ICompanyAreasService>();
            companyAreaServiceMock.Setup(x => x.ListCompanyAreas()).Returns(companyAreaDummies);

            var holderServiceMock = new Mock<IHolderService>();
            holderServiceMock.Setup(x => x.ListHolder()).Returns(holderDummies);

            var toolController = new ToolController(null, null, null, holderServiceMock.Object,
                companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var model = (toolController.CheckinTab(toolId) as ViewResult).Model;
            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(DateTime.Now.Date, ((CheckinToolTabViewModel)model).CheckinDateTime.Value.Date);
            Assert.IsNotNull(((CheckinToolTabViewModel)model).StrCheckinDateTime);
        }
    }
}
