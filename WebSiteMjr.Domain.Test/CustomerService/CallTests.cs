using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model.CustomerService;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.Domain.Test.CustomerService
{
    [TestClass]
    public class CallTests
    {
        [TestMethod]
        public void Given_A_Valid_Call_When_Client_Called_And_Im_Creating_A_New_Call_Then_Create_In_Database()
        {
            var portoverano = CompanyDummies.CreatePortoveranoWithCompanyArea();
            var companyAreasWithProblem = portoverano.CompanyAreas.Take(2);
            var dateOfTheCall = new DateTime(2014, 08, 02, 14, 25, 00);
            var serviceType = ServiceTypeDummies.CreateProblemWithCameras();


            var call = new Call(portoverano, companyAreasWithProblem, "Problema com cameras nesses lugares", 
                "Problema em cameras", serviceType) ;

            var callService = new CallService(_callRepositoryMock.Object, new StubUnitOfWork());


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
