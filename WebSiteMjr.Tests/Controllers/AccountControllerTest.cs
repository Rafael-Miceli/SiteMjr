using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMatrix.WebData;

namespace WebSiteMjr.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        [Ignore]
        public void Should_Login()
        {
            const string userName = "MjrAdmin";
            const string password = "123456";

            var userIsLogged = WebSecurity.Login(userName, password);

            Assert.IsTrue(userIsLogged);
        }
    }
}
