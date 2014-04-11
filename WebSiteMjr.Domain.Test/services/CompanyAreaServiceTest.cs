using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.Domain.Test.services
{
    [TestClass]
    public class CompanyAreaServiceTest
    {
        [TestMethod]
        public void Given_A_New_CompanyArea_When_Creating_With_A_Non_Existent_Name_Then_Add_The_CompanyArea()
        {
            var companyArea = CompanyAreasDummies.CreateOneCompanyArea();
            var companyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());


            companyAreaService.CreateCompanyArea(companyArea);


            Assert.IsNotNull(companyAreaService.FindCompanyArea(companyArea.Id));
        }

        [TestMethod]
        public void Given_An_Existing_CompanyArea_When_Updating_With_A_Non_Existent_Name_Then_Update_The_CompanyArea()// Should_Edit_CompanyArea()
        {
            const string companyAreaNameUpdated = "Valor Atualizado";
            var companyAreaCreated = CompanyAreasDummies.CreateOneCompanyArea();
            var companyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());
            companyAreaService.CreateCompanyArea(companyAreaCreated);

            var companyAreaToUpdate = companyAreaService.FindCompanyArea(companyAreaCreated.Id);
            companyAreaToUpdate.Name = companyAreaNameUpdated;


            companyAreaService.UpdateCompanyArea(companyAreaToUpdate);


            Assert.AreEqual(companyAreaService.FindCompanyArea(companyAreaToUpdate.Id).Name, companyAreaNameUpdated);
        }

        [TestMethod]
        public void Should_Delete_CompanyArea()
        {
            var CompanyAreaCreated = CompanyAreasDummies.CreateOneCompanyArea();
            var CompanyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());
            CompanyAreaService.CreateCompanyArea(CompanyAreaCreated);
            var CompanyAreaToDelete = CompanyAreaService.FindCompanyArea(CompanyAreaCreated.Id);

            CompanyAreaService.DeleteCompanyArea(CompanyAreaToDelete.Id);

            Assert.IsNull(CompanyAreaService.FindCompanyArea(CompanyAreaToDelete.Id));
        }

        [TestMethod]
        public void Should_Find_CompanyArea_By_Name()
        {
            var CompanyAreaCreated = CompanyAreasDummies.CreateOneCompanyArea();
            var CompanyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());
            CompanyAreaService.CreateCompanyArea(CompanyAreaCreated);

            var CompanyAreaFounded = CompanyAreaService.FindCompanyAreaByName(CompanyAreaCreated.Name);

            Assert.AreEqual(CompanyAreaFounded.Name, CompanyAreaCreated.Name);
        }

        [TestMethod]
        public void Should_Link_One_CompanyArea_To_Company()
        {
            var CompanyAreasId = new List<int>();
            var company = CompanyDummies.CreateOneCompany();
            var CompanyArea = CompanyAreasDummies.CreateOneCompanyArea();
            CompanyAreasId.Add(CompanyArea.Id);
            var CompanyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());
            var companyService = new CompanyService(new FakeCompanyRepository(), new StubUnitOfWork());
            CompanyAreaService.CreateCompanyArea(CompanyArea);
            companyService.CreateCompany(company);

            CompanyAreaService.LinkToolsLocalizationToCompany(CompanyAreasId, company);

            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == CompanyArea.Id));
        }

        [TestMethod]
        public void Should_Link_Two_Or_More_CompanyArea_To_Company()
        {
            var CompanyAreaService = new CompanyAreasService(new FakeCompanyAreaRepository(), new StubUnitOfWork());
            var companyService = new CompanyService(new FakeCompanyRepository(), new StubUnitOfWork());
            var CompanyAreasId = new List<int>();
            var company = CompanyDummies.CreateOneCompany();
            var CompanyAreas = CompanyAreaService.ListCompanyAreas().ToList();
            CompanyAreasId.Add(CompanyAreas[0].Id);
            CompanyAreasId.Add(CompanyAreas[1].Id);
            CompanyAreasId.Add(CompanyAreas[2].Id);
            
            companyService.CreateCompany(company);

            CompanyAreaService.LinkToolsLocalizationToCompany(CompanyAreasId, company);

            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == CompanyAreas[0].Id));
            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == CompanyAreas[1].Id));
            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == CompanyAreas[2].Id));
        }

        [TestMethod]
        public void Should_Return_All_Companies_Not_Deleted()
        {
            //Arrange
            var companiesNotDeleted = CompanyDummies.CreateListOfCompanies().Where(e => !e.IsDeleted);
            var companieRepositoryMock = new Mock<ICompanyRepository>();
            companieRepositoryMock.Setup(x => x.GetAllCompaniesNotDeleted()).Returns(companiesNotDeleted);

            var companyService = new CompanyService(companieRepositoryMock.Object, new StubUnitOfWork());

            //Act
            var companies = companyService.ListCompaniesNotDeleted();

            //Assert
            companieRepositoryMock.VerifyAll();
            Assert.IsFalse(companies.Any(e => e.IsDeleted));
        }

    }

    public class FakeCompanyAreaRepository : ICompanyAreaRepository
    {
        private readonly ICollection<CompanyArea> _companyAreas;

        public FakeCompanyAreaRepository()
        {
            _companyAreas = CompanyAreasDummies.CreateListOfCompanyAreas();
        }

        public void Add(CompanyArea companyArea)
        {
            _companyAreas.Add(companyArea);
        }

        public void Remove(object entitie)
        {
            var CompanyAreaToDelete = _companyAreas.FirstOrDefault(t => t.Id == (int)entitie);

            _companyAreas.Remove(CompanyAreaToDelete);
        }

        public void DeleteEntityPermanently(CompanyArea entitieToRemove)
        {
            throw new NotImplementedException();
        }

        public bool ImplementsIsDeletable(CompanyArea entityToRemove)
        {
            throw new NotImplementedException();
        }

        public void MakeEntityDeleted(object entitie, CompanyArea entitieToRemove)
        {
            throw new NotImplementedException();
        }

        public void Update(CompanyArea companyArea)
        {
            var CompanyAreaToUpdate = _companyAreas.FirstOrDefault(t => t.Id == companyArea.Id);

            CompanyAreaToUpdate.Name = CompanyAreaToUpdate.Name;
        }

        public IEnumerable<CompanyArea> GetAll()
        {
            return _companyAreas;
        }

        public IEnumerable<CompanyArea> Query(Func<CompanyArea, bool> filter)
        {
            throw new NotImplementedException();
        }

        public CompanyArea FindEntity(object entityId)
        {
            throw new NotImplementedException();
        }

        public CompanyArea GetById(object id)
        {
            return _companyAreas.FirstOrDefault(t => t.Id == (int)id);
        }

        public CompanyArea Get(Expression<Func<CompanyArea, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public CompanyArea GetByName(string name)
        {
            return _companyAreas.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
