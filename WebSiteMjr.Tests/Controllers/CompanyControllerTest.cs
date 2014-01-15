using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.ViewModels.Company;

namespace WebSiteMjr.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        [TestMethod]
        public void Model_Should_Be_Of_Type_EditCompanyViewModel_In_Get_Edit_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();
            
            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var model = (companyController.Edit(dummieEditCompanyViewModel.Id) as ViewResult).Model;

            //Assert
            Assert.IsInstanceOfType(model, typeof(EditCompanyViewModel));
            companyServiceMock.VerifyAll();
            toolLocalizationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Get_Edit_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var model = (companyController.Edit(dummieEditCompanyViewModel.Id) as ViewResult).Model;

            //Assert
            Assert.AreEqual(dummieCompany.Name, ( (EditCompanyViewModel)model).Name);
            companyServiceMock.VerifyAll();
            toolLocalizationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Post_Edit_Company_With_No_ToolsLocalization_Selected()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            companyServiceMock.Setup(s => s.UpdateCompany(dummieCompany));
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            companyController.Edit(dummieEditCompanyViewModel.Id, dummieEditCompanyViewModel);

            //Assert
            Assert.AreEqual(0, dummieEditCompanyViewModel.ToolsLocalizations.Count(s => s.Selected));
            companyServiceMock.VerifyAll();
            toolLocalizationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Post_Edit_Company_With_ToolsLocalization_Selected()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithToolLocalization();
            var dummieToolsLocaliations = ToolLocalizationDumies.CreateListOfToolsLocalizations();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            companyServiceMock.Setup(s => s.UpdateCompany(dummieCompany));
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            companyController.Edit(dummieEditCompanyViewModel.Id, dummieEditCompanyViewModel);

            //Assert
            Assert.AreEqual(1, dummieEditCompanyViewModel.ToolsLocalizations.Count(s => s.Selected));
            companyServiceMock.VerifyAll();
            toolLocalizationServiceMock.VerifyAll();
        }
    }
}
