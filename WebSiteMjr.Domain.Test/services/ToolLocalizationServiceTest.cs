﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.Domain.Test.services
{
    [TestClass]
    public class ToolLocalizationServiceTest
    {
        [TestMethod]
        public void Should_Create_ToolLocalization()
        {
            var toolLocalization = CompanyAreasDumies.CreateOneToolLocalization();
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());

            toolLocalizationService.CreateToolLocalization(toolLocalization);

            Assert.IsNotNull(toolLocalizationService.FindToolLocalization(toolLocalization.Id));
        }

        [TestMethod]
        public void Should_Edit_ToolLocalization()
        {
            const string toolLocalizatioNameUpdated = "Valor Atualizado";
            var toolLocalizationCreated = CompanyAreasDumies.CreateOneToolLocalization();
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);
            var toolLocalizationToUpdate = toolLocalizationService.FindToolLocalization(toolLocalizationCreated.Id);
            toolLocalizationToUpdate.Name = toolLocalizatioNameUpdated;

            toolLocalizationService.UpdateToolLocalization(toolLocalizationToUpdate);

            Assert.AreEqual(toolLocalizationService.FindToolLocalization(toolLocalizationToUpdate.Id).Name, toolLocalizatioNameUpdated);
        }

        [TestMethod]
        public void Should_Delete_ToolLocalization()
        {
            var toolLocalizationCreated = CompanyAreasDumies.CreateOneToolLocalization();
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);
            var toolLocalizationToDelete = toolLocalizationService.FindToolLocalization(toolLocalizationCreated.Id);

            toolLocalizationService.DeleteToolLocalization(toolLocalizationToDelete.Id);

            Assert.IsNull(toolLocalizationService.FindToolLocalization(toolLocalizationToDelete.Id));
        }

        [TestMethod]
        public void Should_Find_ToolLocalization_By_Name()
        {
            var toolLocalizationCreated = CompanyAreasDumies.CreateOneToolLocalization();
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);

            var toolLocalizationFounded = toolLocalizationService.FindToolLocalizationByName(toolLocalizationCreated.Name);

            Assert.AreEqual(toolLocalizationFounded.Name, toolLocalizationCreated.Name);
        }

        [TestMethod]
        public void Should_Link_One_ToolLocalization_To_Company()
        {
            var toolsLocalizationsId = new List<int>();
            var company = CompanyDummies.CreateOneCompany();
            var toolLocalization = CompanyAreasDumies.CreateOneToolLocalization();
            toolsLocalizationsId.Add(toolLocalization.Id);
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());
            var companyService = new CompanyService(new FakeCompanyRepository(), new StubUnitOfWork());
            toolLocalizationService.CreateToolLocalization(toolLocalization);
            companyService.CreateCompany(company);

            toolLocalizationService.LinkToolsLocalizationToCompany(toolsLocalizationsId, company);

            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == toolLocalization.Id));
        }

        [TestMethod]
        public void Should_Link_Two_Or_More_ToolLocalization_To_Company()
        {
            var toolLocalizationService = new CompanyAreasService(new FakeToolLocalizationRepository(), new StubUnitOfWork());
            var companyService = new CompanyService(new FakeCompanyRepository(), new StubUnitOfWork());
            var toolsLocalizationsId = new List<int>();
            var company = CompanyDummies.CreateOneCompany();
            var toolsLocalizations = toolLocalizationService.ListToolsLocalizations().ToList();
            toolsLocalizationsId.Add(toolsLocalizations[0].Id);
            toolsLocalizationsId.Add(toolsLocalizations[1].Id);
            toolsLocalizationsId.Add(toolsLocalizations[2].Id);
            
            companyService.CreateCompany(company);

            toolLocalizationService.LinkToolsLocalizationToCompany(toolsLocalizationsId, company);

            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == toolsLocalizations[0].Id));
            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == toolsLocalizations[1].Id));
            Assert.IsNotNull(companyService.FindCompany(company.Id).CompanyAreas.FirstOrDefault(tl => tl.Id == toolsLocalizations[2].Id));
        }

    }

    public class FakeToolLocalizationRepository : IToolLocalizationRepository
    {
        private readonly ICollection<CompanyArea> _toolLocalizations;

        public FakeToolLocalizationRepository()
        {
            _toolLocalizations = CompanyAreasDumies.CreateListOfCompanyAreas();
        }

        public void Add(CompanyArea companyArea)
        {
            _toolLocalizations.Add(companyArea);
        }

        public void Remove(object entitie)
        {
            var toolLocalizationToDelete = _toolLocalizations.FirstOrDefault(t => t.Id == (int)entitie);

            _toolLocalizations.Remove(toolLocalizationToDelete);
        }

        public void Update(CompanyArea companyArea)
        {
            var toolLocalizationToUpdate = _toolLocalizations.FirstOrDefault(t => t.Id == companyArea.Id);

            toolLocalizationToUpdate.Name = toolLocalizationToUpdate.Name;
        }

        public IEnumerable<CompanyArea> GetAll()
        {
            return _toolLocalizations;
        }

        public IEnumerable<CompanyArea> Query(Func<CompanyArea, bool> filter)
        {
            throw new NotImplementedException();
        }

        public CompanyArea GetById(object id)
        {
            return _toolLocalizations.FirstOrDefault(t => t.Id == (int)id);
        }

        public CompanyArea Get(Expression<Func<CompanyArea, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public CompanyArea GetByName(string name)
        {
            return _toolLocalizations.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
