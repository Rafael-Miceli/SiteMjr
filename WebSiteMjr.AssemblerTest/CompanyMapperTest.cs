using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.services;
using WebSiteMjr.EfBaseData.Context;
using WebSiteMjr.EfConfigurationMigrationData;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.AssemblerTest
{
    [TestClass]
    public class CompanyMapperTest
    {
        [TestMethod]
        public void Should_Return_Companies()
        {
            var personUow = new PersonsUow();
            var companyMapper = new CompanyMapper(new CompanyService(new CompanyRepository(personUow), personUow));

            var companies = companyMapper.CompanyToListCompanyViewModel();

            foreach (var company in companies.Companies)
            {
                Console.WriteLine(company.Name);
                Console.WriteLine(company.Email);
            }

            Assert.AreNotEqual(0, companies.Companies.Count());
        }

        [TestMethod]
        public void Should_Return_Companies_In_Context()
        {
            var context = new MjrSolutionContext();
            var personsContext = new PersonsContext();
            //var personUow = new PersonsUow();
            //var companyMapper = new CompanyMapper(new CompanyService(new CompanyRepository(personUow), personUow));

            var companies = personsContext.Companies;//companyMapper.CompanyToListCompanyViewModel();

            foreach (var company in companies)
            {
                Console.WriteLine(company.Name);
                Console.WriteLine(company.Email);
            }

            Assert.AreNotEqual(0, companies.Count());
        }
    }
}
