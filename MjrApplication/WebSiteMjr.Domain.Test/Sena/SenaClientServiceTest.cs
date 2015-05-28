using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.services.Sena;
using WebSiteMjr.SenaData;

namespace WebSiteMjr.Domain.Test.Sena
{
    [TestClass]
    public class SenaClientServiceTest
    {
        [TestMethod]
        public void When_Creating_A_Client_Validate_If_Already_Exists()
        {
            SenaClientRepository senaClientRepository = new SenaClientRepository();
            SenaClientService senaClientService = new SenaClientService(senaClientRepository);

            senaClientService.Create("Demonstração");

            Assert.IsTrue(true);
        }
    }
}
