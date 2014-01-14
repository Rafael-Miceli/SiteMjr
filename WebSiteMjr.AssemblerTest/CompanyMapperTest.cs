using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.EfBaseData.Context;
using WebSiteMjr.EfConfigurationMigrationData;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;
using Moq;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class CompanyMapperTest
    {

        [TestMethod]
        public void should_Return_EditCompanyViewModel_With_ToolsLocalization_Data()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var company = CompanyDumies.CreateOneCompany();

            companyServiceMock.Setup(x => x.FindCompany(It.IsAny<Company>())).Returns(company);
            toolLocalizationServiceMock.Setup(x => x.ListToolsLocalizations())
                .Returns(ToolLocalizationDumies.CreateListOfToolsLocalizations);

            var companyMapper = new CompanyMapper(companyServiceMock.Object, toolLocalizationServiceMock.Object);

            //Act
            var companyViewModel = companyMapper.CompanyToEditCompanyViewModel(company);

            //Assert
            toolLocalizationServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Map_ToolsLocalization_In_Company_To_View_When_Company_Doesnt_Have_Any_ToolLocalization_Mapped()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var toolLocalizationServiceMock = new Mock<IToolLocalizationService>();
            var dummieCompany = CompanyDumies.CreateOneCompany();
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
            var dummieCompany = CompanyDumies.CreateOneCompanyWithToolLocalization();
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
        public void Should_Map_Company_To_CompanyViewModel_Edited()
        {
            
        }
    }

}
