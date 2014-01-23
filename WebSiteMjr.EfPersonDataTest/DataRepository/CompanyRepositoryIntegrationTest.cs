using System;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfPersonDataTest.DataRepository
{
    [TestClass]
    public class CompanyRepositoryIntegrationTest
    {
        [TestMethod]
        [Ignore]
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
        [Ignore]
        public void Should_Update_ToolsLocalization_In_Company()
        {
            try
            {
                var personContext = new PersonsContext();
                var uow = new UnitOfWork<PersonsContext>();
                var companyRepository = new CompanyRepository(uow);
                var CompanyAreaRepository = new CompanyAreaRepository(uow);

                var CompanyArea =
                    CompanyAreasDumies.CreateListOfCompanyAreas().FirstOrDefault(t => t.Id == 2);
                var company = companyRepository.GetById(4);

                CompanyAreaRepository.Add(CompanyArea);
                uow.Save();
                company.CompanyAreas.Add(CompanyAreaRepository.GetById(6));
                companyRepository.Update(company);
                uow.Save();

                Assert.IsNotNull(personContext.Companies.FirstOrDefault(c => c.Name == "Portoverano"));
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine(("Property: {0} Error: {1}"), validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((" Error: {0}"), ex.Message);
            }
        }

        [TestMethod]
        [Ignore]
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
