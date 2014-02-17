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
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();
            
            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            var model = (companyController.Edit(dummieEditCompanyViewModel.Id) as ViewResult).Model;

            //Assert
            Assert.IsInstanceOfType(model, typeof(EditCompanyViewModel));
            companyServiceMock.VerifyAll();
            CompanyAreaServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Get_Edit_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            var model = (companyController.Edit(dummieEditCompanyViewModel.Id) as ViewResult).Model;

            //Assert
            Assert.AreEqual(dummieCompany.Name, ( (EditCompanyViewModel)model).Name);
            companyServiceMock.VerifyAll();
            CompanyAreaServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Post_Edit_Company_With_No_ToolsLocalization_Selected()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModel();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            companyServiceMock.Setup(s => s.UpdateCompany(dummieCompany));
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            companyController.Edit(dummieEditCompanyViewModel.Id, dummieEditCompanyViewModel);

            //Assert
            Assert.AreEqual(0, dummieEditCompanyViewModel.CompanyAreas.Count(s => s.Selected));
            companyServiceMock.VerifyAll();
            CompanyAreaServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Post_Edit_Company_With_ToolsLocalization_Selected()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var CompanyAreaServiceMock = new Mock<ICompanyAreasService>();

            var dummieCompany = CompanyDummies.CreateOneCompany();
            var dummieEditCompanyViewModel = CompanyDummies.CreateOneEditCompanyViewModelWithCompanyArea();
            var dummieToolsLocaliations = CompanyAreasDummies.CreateListOfCompanyAreas();

            companyServiceMock.Setup(s => s.FindCompany(dummieEditCompanyViewModel.Id)).Returns(dummieCompany);
            companyServiceMock.Setup(s => s.UpdateCompany(dummieCompany));
            CompanyAreaServiceMock.Setup(x => x.ListCompanyAreas())
                .Returns(dummieToolsLocaliations);

            var companyController = new CompanyController(companyServiceMock.Object, CompanyAreaServiceMock.Object);

            //Act
            companyController.Edit(dummieEditCompanyViewModel.Id, dummieEditCompanyViewModel);

            //Assert
            Assert.AreEqual(1, dummieEditCompanyViewModel.CompanyAreas.Count(s => s.Selected));
            companyServiceMock.VerifyAll();
            CompanyAreaServiceMock.VerifyAll();
        }
    }
}
