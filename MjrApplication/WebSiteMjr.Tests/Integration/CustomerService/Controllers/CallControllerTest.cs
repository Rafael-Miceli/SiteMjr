using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Areas.CustomerService.Controllers;

namespace WebSiteMjr.Tests.Integration.CustomerService.Controllers
{
    [TestClass]
    public class CallControllerTest
    {
        [TestMethod]
        [Ignore]
        public void Given_A_Request_To_Inde_Page_When_Calling_Index_Page_Then_Return_List_Of_Pedent_And_IntoService_Calls()
        {
            var callController = new CallController(null, null);

            var result = callController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
