using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class CheckinToolMapperTest
    {
        private CheckinToolMapper _checkinToolMapper;
        private Mock<IHolderService> _holderServiceMock;
        private Mock<IToolService> _toolServiceMock;
        private Mock<ICheckinToolService> _checkinToolServiceMock;
        private Mock<ICompanyAreasService> _companyAreaServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _holderServiceMock = new Mock<IHolderService>();
            _toolServiceMock = new Mock<IToolService>();
            _checkinToolServiceMock = new Mock<ICheckinToolService>();
            _companyAreaServiceMock = new Mock<ICompanyAreasService>();

            _checkinToolMapper = new CheckinToolMapper(_checkinToolServiceMock.Object, _toolServiceMock.Object, _holderServiceMock.Object, _companyAreaServiceMock.Object);
        }

        [TestMethod]
        public void Should_Map_CreateCheckinToolViewModel_To_CheckinTool_When_All_Fields_Are_Populated()
        {
            //Arrange
            var createCheckinToolViewModel = CheckinToolDummies.CreateOneCheckinToolViewModel();

            ArrangeCheckinToolViewModel();

            //Act
            var checkinTool = _checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            _checkinToolServiceMock.VerifyAll();
            _toolServiceMock.VerifyAll();
            _holderServiceMock.VerifyAll();
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

            ArrangeCheckinToolViewModel();

            //Act
            var checkinTool = _checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            _checkinToolServiceMock.VerifyAll();
            _toolServiceMock.VerifyAll();
            _holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinTool.EmployeeCompanyHolderId);
            Assert.IsNotNull(checkinTool.Tool);
            Assert.IsNotNull(checkinTool.CheckinDateTime);
            Assert.IsNull(checkinTool.CompanyAreaId);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ObjectNotExistsException<Holder>))]
        public void Should_Raise_Exception_And_Not_Map_CreateCheckinToolViewModel_To_CheckinTool_When_Holder_Is_Marked_As_Deleted()
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
                Name = "Portão"
            };

            _holderServiceMock.Setup(x => x.FindHolderByName(employee.Name))
                .Returns(employee);
            _toolServiceMock.Setup(x => x.FindToolByName(tool.Name))
                .Returns(tool);
            _companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(companyArea.Name))
                .Returns(companyArea);

            //Act
            _checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotExistsException<Tool>))]
        public void Should_Raise_Exception_And_Not_Map_CreateCheckinToolViewModel_To_CheckinTool_When_Tool_Is_Marked_As_Deleted()
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
                Name = "Ferramenta 1",
                IsDeleted = true
            };

            var companyArea = new CompanyArea
            {
                Id = 1,
                Name = "Portão"
            };

            _holderServiceMock.Setup(x => x.FindHolderByName(employee.Name))
                .Returns(employee);
            _toolServiceMock.Setup(x => x.FindToolByName(tool.Name))
                .Returns(tool);
            _companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(companyArea.Name))
                .Returns(companyArea);

            //Act
            _checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(createCheckinToolViewModel);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Should_Map_CheckinTool_To_CreateCheckinToolViewModel_When_All_Fields_Are_Populated()
        {
            //Arrange
            var createCheckinTool = CheckinToolDummies.CreateOneCheckinTool();

            ArrangeCheckinTool();

            //Act
            var checkinToolViewModel = _checkinToolMapper.CheckinToolToCreateCheckinToolViewModel(createCheckinTool);

            //Assert
            _checkinToolServiceMock.VerifyAll();
            _holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinToolViewModel.EmployeeCompanyHolderName);
            Assert.IsNotNull(checkinToolViewModel.ToolName);
            Assert.IsNotNull(checkinToolViewModel.StrCheckinDateTime);
            Assert.IsNotNull(checkinToolViewModel.CompanyAreaName);
        }

        [TestMethod]
        public void Should_Map_CheckinTool_To_CreateCheckinToolViewModel_When_CompanyAreaId_Is_Null()
        {
            //Arrange
            var createCheckinTool = CheckinToolDummies.CreateOneCheckinToolWithoutCompanyArea();

            ArrangeCheckinTool();

            //Act
            var checkinToolViewModel = _checkinToolMapper.CheckinToolToCreateCheckinToolViewModel(createCheckinTool);

            //Assert
            _checkinToolServiceMock.VerifyAll();
            _holderServiceMock.VerifyAll();
            Assert.IsNotNull(checkinToolViewModel.EmployeeCompanyHolderName);
            Assert.IsNotNull(checkinToolViewModel.ToolName);
            Assert.IsNotNull(checkinToolViewModel.StrCheckinDateTime);
            Assert.IsNull(checkinToolViewModel.CompanyAreaName);
        }

        private void ArrangeCheckinToolViewModel()
        {
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
                Name = "Portão"
            };

            _holderServiceMock.Setup(x => x.FindHolderByName(employee.Name))
                .Returns(employee);
            _toolServiceMock.Setup(x => x.FindToolByName(tool.Name))
                .Returns(tool);
            _companyAreaServiceMock.Setup(x => x.FindCompanyAreaByName(companyArea.Name))
                .Returns(companyArea);
        }

        private void ArrangeCheckinTool()
        {
            var employee = new Employee
            {
                Id = 1,
                Name = "Celso"
            };

            var companyArea = new CompanyArea
            {
                Id = 1,
                Name = "Portão"
            };

            _holderServiceMock.Setup(x => x.FindHolder(employee.Id))
                .Returns(employee);
            _companyAreaServiceMock.Setup(x => x.FindCompanyArea(companyArea.Id))
                .Returns(companyArea);
        }
    }
}
