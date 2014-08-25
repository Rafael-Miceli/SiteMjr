using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.Domain.Test.CustomerService
{
    [TestClass]
    public class CallTests
    {
        private Mock<ICallRepository> _callRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _callRepositoryMock = new Mock<ICallRepository>();
        }

        [TestMethod]
        public void Given_A_Valid_Call_When_Client_Called_And_Im_Creating_A_New_Call_Then_Create_In_Database()
        {
            _callRepositoryMock.Setup(x => x.Add(It.IsAny<Call>()));

            var portoverano = CompanyDummies.CreatePortoveranoWithCompanyArea();
            var companyAreasWithProblem = portoverano.CompanyAreas.Take(2);
            var serviceType = ServiceTypeDummies.CreateProblemWithCameras();


            var call = new Call(portoverano, companyAreasWithProblem, "Problema com cameras nesses lugares", 
                "Problema em cameras", serviceType) ;

            var callService = new CallService(_callRepositoryMock.Object, new StubUnitOfWork());

            callService.CreateCall(call);

            _callRepositoryMock.VerifyAll();
        }
    }

    public static class ServiceTypeDummies
    {
        public static ServiceType CreateProblemWithCameras()
        {
            return new CameraServiceModel
            {
                Details = "Cameras"
            };
        }
    }
}
