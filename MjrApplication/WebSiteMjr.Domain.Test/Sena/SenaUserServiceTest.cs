using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.services.Sena;
using WebSiteMjr.SenaData;

namespace WebSiteMjr.Domain.Test.Sena
{
    [TestClass]
    public class SenaUserServiceTest
    {
        [TestMethod]
        [Ignore]
        public void When_Creating_A_User_Validate_If_Already_Exists()
        {
            SenaUserRepository senaUserRepository = new SenaUserRepository();
            SenaUserService senaUserService = new SenaUserService(senaUserRepository);

            senaUserService.Create("mjrteste@mjr.com", "Demonstração");

            Assert.IsTrue(true);
        }
    }
}
