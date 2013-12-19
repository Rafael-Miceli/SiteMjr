using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfPersonDataTest.DataRepository
{
    [TestClass]
    public class CompanyRepositoryTest
    {
        [TestMethod]
        public void Should_Create_Company()
        {
            var personContext = new PersonsContext();
            //var uow = new UnitOfWork<PersonsContext>();
            //var companyRepository = new CompanyRepository(uow);
            var company = new Company
            {
                Name = "P",
                Email = "P"
            };

            personContext.Companies.Add(company);
            personContext.SaveChanges();
            //companyRepository.Add(company);
            //uow.Save();

            Assert.Fail("Failed");
        }
    }
}
