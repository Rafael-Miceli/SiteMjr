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
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var company = CompanyDummies.CreatePortofinoCompany();

            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(CompanyAreasDummies.CreateListOfCompanyAreas);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            companyMapper.CompanyToEditCompanyViewModel(company);

            //Assert
            CompanyAreaServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Map_ToolsLocalization_In_Company_To_View_When_Company_Doesnt_Have_Any_CompanyArea_Mapped()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var dummieCompany = CompanyDummies.CreatePortofinoCompany();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();

            companyServiceMock.Setup(x => x.FindCompany(It.IsAny<Company>())).Returns(dummieCompany);
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            var companyViewModel = companyMapper.CompanyToEditCompanyViewModel(dummieCompany);

            //Assert
            CompanyAreaServiceMock.VerifyAll();
            Assert.AreEqual(companyViewModel.CompanyAreas.Count(), dummieToolsLocaliations.Count);
            Assert.IsFalse(companyViewModel.CompanyAreas.Any(t => t.Selected));
        }

        [TestMethod]
        public void Map_ToolsLocalization_In_Company_To_View_When_Company_Have_Any_CompanyArea_Mapped()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var dummieCompany = CompanyDummies.CreatePortoveranoWithCompanyArea();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();

            companyServiceMock.Setup(x => x.FindCompany(It.IsAny<Company>())).Returns(dummieCompany);
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            var companyViewModel = companyMapper.CompanyToEditCompanyViewModel(dummieCompany);

            //Assert
            CompanyAreaServiceMock.VerifyAll();
            Assert.AreEqual(companyViewModel.CompanyAreas.Count(), dummieToolsLocaliations.Count);
            Assert.IsTrue(companyViewModel.CompanyAreas.Any(t => t.Selected));
        }

        [TestMethod]
        public void Should_Map_Company_To_EditedCompanyViewModel()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var companyShouldReturn = CompanyDummies.CreatePortofinoCompany();

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);
            
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
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithCompanyArea();
            var companyShouldReturn = CompanyDummies.CreatePortofinoCompany();
            var actualCountBeforeUpdate = companyShouldReturn.CompanyAreas.Count;

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            var companyMapped = companyMapper.EditCompanyViewModelToCompany(dummieCompanyViewModel);

            //Assert
            Assert.AreEqual(0, actualCountBeforeUpdate);
            Assert.AreEqual(dummieCompanyViewModel.CompanyAreas.Count(c => c.Selected), companyMapped.CompanyAreas.Count);
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Map_EditedCompanyViewModel_To_Company_With_No_CompanyArea_Registered()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();
            var dummieCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithNoCompanyArea();
            var companyShouldReturn = CompanyDummies.CreatePortofinoCompany();

            companyServiceMock.Setup(s => s.FindCompany(dummieCompanyViewModel.Id)).Returns(companyShouldReturn);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            companyMapper.EditCompanyViewModelToCompany(dummieCompanyViewModel);

            //Assert
            companyServiceMock.VerifyAll();
        }
    }

}
