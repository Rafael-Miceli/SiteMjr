using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void ShouldReturnUserCompany()
        {
            var fakeUser = 


            var fakeCompanyId = fakeUser.CompanyId;

            var fakeCompanyReturned = fakeCompanyService.FindCompany(fakeCompanyId);

            Assert.IsTrue(fakeCompanyReturned.Id == fakeCompanyId);
        }
    }
}
