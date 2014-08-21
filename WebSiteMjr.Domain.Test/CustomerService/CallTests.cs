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
            var problemType = ServiceTypeDummies.CreateProblemWithCameras();


            var call = new Call
            {
                Company = portoverano,
                CompanyAreas = companyAreasWithProblem,
                Description = "Problema com cameras nesses lugares",
                Title = "Problema em cameras",
                DateCreated = dateOfTheCall,
                ProblemType = problemType
            };
        }
    }

    public static class ServiceTypeDummies
    {
        public static ServiceType CreateProblemWithCameras()
        {
            return new CameraService
            {
                Details = "Cameras"
            };
        }
    }
}
