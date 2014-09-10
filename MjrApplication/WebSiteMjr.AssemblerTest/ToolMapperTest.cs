using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.Domain.Test.services;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class ToolMapperTest
    {
        [TestMethod]
        public void Should_Map_Checkins_Of_Tool_To_CheckinToolTabViewModel_With_CheckinTools()
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

            var toolMapper = new ToolMapper(null, null, null, holderServiceMock.Object, companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var checkinToolTabViewModel = toolMapper.CheckinsOfThisToolToCheckinToolTabViewModel(toolId);

            //Assert
            Assert.IsNotNull(checkinToolTabViewModel);
            Assert.IsNotNull(checkinToolTabViewModel.CheckinTools);
            Assert.IsTrue(checkinToolTabViewModel.CheckinTools.Any());
        }

        [TestMethod]
        public void Should_Have_Today_Date_CheckinToolTabViewModel()
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

            var toolMapper = new ToolMapper(null, null, null, holderServiceMock.Object, companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var checkinToolTabViewModel = toolMapper.CheckinsOfThisToolToCheckinToolTabViewModel(toolId);

            //Assert
            Assert.IsNotNull(checkinToolTabViewModel);
            Assert.IsTrue(DateTime.Now.Date == checkinToolTabViewModel.CheckinDateTime.Value.Date);
        }

        [TestMethod]
        public void Should_Map_CheckinToolTabViewModel_To_CheckinTool_When_All_Fields_Are_Populated()
        {
            //Arrange
            var employee = new Employee
            {
                Id = 1,
                Name = "Celso"
            };

            var tool = new Tool
            {
                Id = 1,
                Name = "Ferramenta 1"
            };

            var companyArea = new CompanyArea
            {
                Id = 1,
                Name = "Portões"
            };

            var createCheckinToolTabViewModel = CheckinToolDummies.CreateOneCheckinToolTabViewModel(tool.Id);
            
            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolTabViewModel.HolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindTool(createCheckinToolTabViewModel.ToolId))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(createCheckinToolTabViewModel.CompanyAreaName))
                .Returns(companyArea);

            var toolMapper = new ToolMapper(null, null, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var checkinTool = toolMapper.MapCheckinToolTabViewModelToCheckinTool(createCheckinToolTabViewModel);

            //Assert
            checkinToolServiceMock.VerifyAll();
            toolServiceMock.VerifyAll();
            holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinTool.EmployeeCompanyHolderId);
            Assert.IsNotNull(checkinTool.Tool);
            Assert.IsNotNull(checkinTool.CheckinDateTime);
            Assert.IsNotNull(checkinTool.CompanyAreaId);
        }

        [TestMethod]
        public void Should_Map_CheckinToolTabViewModel_To_CheckinTool_When_CompanyArea_Is_Null()
        {
            //Arrange
            var employee = new Employee
            {
                Id = 1,
                Name = "Celso"
            };

            var tool = new Tool
            {
                Id = 1,
                Name = "Ferramenta 1"
            };

            var companyArea = new CompanyArea
            {
                Id = 1,
                Name = "Portões"
            };

            var createCheckinToolTabViewModel = CheckinToolDummies.CreateOneCheckinToolTabViewModelWithoutCompanyArea(tool.Id);

            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolTabViewModel.HolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindTool(createCheckinToolTabViewModel.ToolId))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(It.IsNotNull<string>()))
                .Returns(companyArea);

            var toolMapper = new ToolMapper(null, null, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object, checkinToolServiceMock.Object);

            //Act
            var checkinTool = toolMapper.MapCheckinToolTabViewModelToCheckinTool(createCheckinToolTabViewModel);

            //Assert
            checkinToolServiceMock.VerifyAll();
            toolServiceMock.VerifyAll();
            holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinTool.EmployeeCompanyHolderId);
            Assert.IsNotNull(checkinTool.Tool);
            Assert.IsNotNull(checkinTool.CheckinDateTime);
            Assert.IsNull(checkinTool.CompanyAreaId);
        }
    }
}
