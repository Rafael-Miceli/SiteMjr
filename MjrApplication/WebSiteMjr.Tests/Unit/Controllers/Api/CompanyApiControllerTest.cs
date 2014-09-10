using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Controllers.Api;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Tests.Controllers.Api
{
    [TestClass]
    public class CompanyApiControllerTest
    {
        [TestMethod]
        public void Should_List_CompanyAreas_From_Selected_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var companyNames = new List<string>
            {
                 "Portão de visitantes"
            };

            companyServiceMock.Setup(x => x.FindCompanyCompanyAreasNames(It.Is<string>(s => s == "Portomare"))).Returns(companyNames);

            var companyApiController = new CompanyApiController(companyServiceMock.Object);

            //Act
            var companyAreas = companyApiController.ListCompanyAreas("Portomare");

            //Assert
            Assert.IsNotNull(companyAreas);
            Assert.IsTrue(companyAreas.Any());
        }

        [TestMethod]
        public void Should_Return_Null_When_Dont_Exists_CompanyAreas_From_Selected_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();
            var companyNames = new List<string>
            {
                 "Portão de visitantes"
            };

            companyServiceMock.Setup(x => x.FindCompanyCompanyAreasNames(It.Is<string>(s => s == "Portomare"))).Returns(companyNames);

            var companyApiController = new CompanyApiController(companyServiceMock.Object);

            //Act
            var companyAreas = companyApiController.ListCompanyAreas("Portoverano");

            //Assert
            Assert.IsNotNull(companyAreas);
            Assert.IsFalse(companyAreas.Any());
        }
    }
}
