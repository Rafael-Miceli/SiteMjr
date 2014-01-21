using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Test.Model;
using Moq;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class CompanyMapperTest
    {

        [TestMethod]
        public void Should_Return_EditCompanyViewModel_With_ToolsLocalization_Data()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var company = CompanyDummies.CreateOneCompany();

            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(ToolLocalizationDumies.CreateListOfToolsLocalizations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            companyMapper.CompanyToEditCompanyViewModel(company);

            //Assert
            toolLocalizationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Map_ToolsLocalization_In_Company_To_View_When_Company_Doesnt_Have_Any_ToolLocalization_Mapped()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();

            companyServiceMock.Setup(x => x.FindCompany(It.IsAny<Company>())).Returns(dummieCompany);
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var companyViewModel = companyMapper.CompanyToEditCompanyViewModel(dummieCompany);

            //Assert
            toolLocalizationServiceMock.VerifyAll();
            Assert.AreEqual(companyViewModel.ToolsLocalizations.Count(), dummieToolsLocaliations.Count);
            Assert.IsFalse(companyViewModel.ToolsLocalizations.Any(t => t.Selected));
        }

        [TestMethod]
        public void Map_ToolsLocalization_In_Company_To_View_When_Company_Have_Any_ToolLocalization_Mapped()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompany = CompanyDummies.CreateOneCompanyWithToolLocalization();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();

            companyServiceMock.Setup(x => x.FindCompany(It.IsAny<Company>())).Returns(dummieCompany);
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var companyViewModel = companyMapper.CompanyToEditCompanyViewModel(dummieCompany);

            //Assert
            toolLocalizationServiceMock.VerifyAll();
            Assert.AreEqual(companyViewModel.ToolsLocalizations.Count(), dummieToolsLocaliations.Count);
            Assert.IsTrue(companyViewModel.ToolsLocalizations.Any(t => t.Selected));
        }

        [TestMethod]
        public void Should_Map_Company_To_EditedCompanyViewModel()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var companyShouldReturn = CompanyDummies.CreateOneCompany();

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);
            
            //Act
            var companyMapped = companyMapper.EditCompanyViewModelToCompany(dummieCompanyViewModel);

            //Assert
            Assert.IsNotNull(companyMapped);
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Map_And_Associate_New_ToolsLocalization_Created_In_EditedCompanyViewModel_To_Exisiting_Company_That_never_Had_Any_ToolsLocalization_Associated_Before()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithToolLocalization();
            var companyShouldReturn = CompanyDummies.CreateOneCompany();
            var actualCountBeforeUpdate = companyShouldReturn.ToolsLocalizations.Count;

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var companyMapped = companyMapper.EditCompanyViewModelToCompany(dummieCompanyViewModel);

            //Assert
            Assert.AreEqual(0, actualCountBeforeUpdate);
            Assert.AreEqual(dummieCompanyViewModel.ToolsLocalizations.Count(c => c.Selected), companyMapped.ToolsLocalizations.Count);
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Map_EditedCompanyViewModel_To_Company_With_No_ToolLocalization_Registered()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithNoToolLocalization();
            var companyShouldReturn = CompanyDummies.CreateOneCompany();

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            companyMapper.EditCompanyViewModelToCompany(dummieCompanyViewModel);

            //Assert
            companyServiceMock.VerifyAll();
        }
    }

}
