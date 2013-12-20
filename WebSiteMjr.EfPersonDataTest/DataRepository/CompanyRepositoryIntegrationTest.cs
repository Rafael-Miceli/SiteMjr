using System;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfPersonDataTest.DataRepository
{
    [TestClass]
    public class CompanyRepositoryIntegrationTest
    {
        [TestMethod]
        public void Should_Create_Company()
        {
            try
            {
                var personContext = new PersonsContext();
                var uow = new UnitOfWork<PersonsContext>();
                var companyRepository = new CompanyRepository(uow);
                var company = new Company
                {
                    Name = "P",
                    Email = "adm@portoverano.com"
                };

                companyRepository.Add(company);

                Assert.IsNotNull(personContext.Companies.FirstOrDefault(c => c.Name == "P"));
            }
            catch (DbEntityValidationException dbEx) 
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                         Console.WriteLine(("Property: {0} Error: {1}"), validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void Should_Get_Company()
        {
            try
            {
                var uow = new UnitOfWork<PersonsContext>();
                var companyRepository = new CompanyRepository(uow);

                var companies = companyRepository.GetAll();
                
                Assert.IsNotNull(companies);
                Assert.IsTrue(companies.Any());
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
