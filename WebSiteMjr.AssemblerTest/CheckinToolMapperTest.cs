using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class CheckinToolMapperTest
    {
        [TestMethod]
        public void Should_Map_CreateCheckinToolViewModel_To_CheckinTool_When_All_Fields_Are_Populated()
        {
            //Arrange
            var createCheckinToolViewModel = CheckinToolDummies.CreateOneCheckinToolViewModel();
            
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

            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindToolByName(createCheckinToolViewModel.ToolName))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(createCheckinToolViewModel.CompanyAreaName))
                .Returns(companyArea);

            var checkinToolMapper = new CheckinToolMapper(checkinToolServiceMock.Object, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object);

            //Act
            var checkinTool = checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

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
        public void Should_Map_CreateCheckinToolViewModel_To_CheckinTool_When_CompanyArea_Is_Null()
        {
            //Arrange
            var createCheckinToolViewModel = CheckinToolDummies.CreateOneCheckinToolViewModelWithoutCompanyArea();

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

            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindToolByName(createCheckinToolViewModel.ToolName))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(It.IsNotNull<string>()))
                .Returns(companyArea);

            var checkinToolMapper = new CheckinToolMapper(checkinToolServiceMock.Object, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object);

            //Act
            var checkinTool = checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            checkinToolServiceMock.VerifyAll();
            toolServiceMock.VerifyAll();
            holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinTool.EmployeeCompanyHolderId);
            Assert.IsNotNull(checkinTool.Tool);
            Assert.IsNotNull(checkinTool.CheckinDateTime);
            Assert.IsNull(checkinTool.CompanyAreaId);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ObjectNotExistsException<Holder>))]
        public void Should_Not_Map_CreateCheckinToolViewModel_To_CheckinTool_When_Holder_Is_Marked_As_Deleted()
        {
            var createCheckinToolViewModel = CheckinToolDummies.CreateOneCheckinToolViewModel();

            var employee = new Employee
            {
                Id = 1,
                Name = "Celso",
                IsDeleted = true
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

            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindToolByName(createCheckinToolViewModel.ToolName))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(createCheckinToolViewModel.CompanyAreaName))
                .Returns(companyArea);

            var checkinToolMapper = new CheckinToolMapper(checkinToolServiceMock.Object, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object);

            //Act
            var checkinTool = checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            checkinToolServiceMock.VerifyAll();
            toolServiceMock.VerifyAll();
            holderServiceMock.VerifyAll();
            Assert.IsNull(checkinTool);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotExistsException<Tool>))]
        public void Should_Not_Map_CreateCheckinToolViewModel_To_CheckinTool_When_Tool_Is_Marked_As_Deleted()
        {
            var createCheckinToolViewModel = CheckinToolDummies.CreateOneCheckinToolViewModel();

            var employee = new Employee
            {
                Id = 1,
                Name = "Celso"
            };

            var tool = new Tool
            {
                Id = 1,
                Name = "Ferramenta 1",
                IsDeleted = true
            };

            var companyArea = new CompanyArea
            {
                Id = 1,
                Name = "Portões"
            };

            var holderServiceMock = new Mock<IHolderService>();
            var toolServiceMock = new Mock<IToolService>();
            var checkinToolServiceMock = new Mock<ICheckinToolService>();
            var companyAreaServiceMock = new Mock<ICompanyAreasService>();

            holderServiceMock.Setup(x => x.FindHolderByName(createCheckinToolViewModel.EmployeeCompanyHolderName))
                .Returns(employee);
            toolServiceMock.Setup(x => x.FindToolByName(createCheckinToolViewModel.ToolName))
                .Returns(tool);
            companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(createCheckinToolViewModel.CompanyAreaName))
                .Returns(companyArea);

            var checkinToolMapper = new CheckinToolMapper(checkinToolServiceMock.Object, toolServiceMock.Object, holderServiceMock.Object, companyAreaServiceMock.Object);

            //Act
            var checkinTool = checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            checkinToolServiceMock.VerifyAll();
            toolServiceMock.VerifyAll();
            holderServiceMock.VerifyAll();
            Assert.IsNull(checkinTool);
        }
    }
}
