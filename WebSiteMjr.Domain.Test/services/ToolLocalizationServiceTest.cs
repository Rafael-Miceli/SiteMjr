using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var toolLocalization = ToolLocalizationDumies.CreateOneToolLocalization();
            var toolLocalizationService = new ToolLocalizationService(new FakeToolLocalizationRepository());

            toolLocalizationService.CreateToolLocalization(toolLocalization);

            Assert.IsNotNull(toolLocalizationService.FindToolLocalization(toolLocalization.Id));
        }

        [TestMethod]
        public void Should_Edit_ToolLocalization()
        {
            const string toolLocalizatioNameUpdated = "Valor Atualizado";
            var toolLocalizationCreated = ToolLocalizationDumies.CreateOneToolLocalization();
            var toolLocalizationService = new ToolLocalizationService(new FakeToolLocalizationRepository());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);
            var toolLocalizationToUpdate = toolLocalizationService.FindToolLocalization(toolLocalizationCreated.Id);
            toolLocalizationToUpdate.Name = toolLocalizatioNameUpdated;

            toolLocalizationService.UpdateToolLocalization(toolLocalizationToUpdate);

            Assert.AreEqual(toolLocalizationService.FindToolLocalization(toolLocalizationToUpdate.Id).Name, toolLocalizatioNameUpdated);
        }

        [TestMethod]
        public void Should_Delete_ToolLocalization()
        {
            var toolLocalizationCreated = ToolLocalizationDumies.CreateOneToolLocalization();
            var toolLocalizationService = new ToolLocalizationService(new FakeToolLocalizationRepository());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);
            var toolLocalizationToDelete = toolLocalizationService.FindToolLocalization(toolLocalizationCreated.Id);

            toolLocalizationService.DeleteToolLocalization(toolLocalizationToDelete.Id);

            Assert.IsNull(toolLocalizationService.FindToolLocalization(toolLocalizationToDelete.Id));
        }

        [TestMethod]
        public void Should_Find_ToolLocalization_By_Name()
        {
            var toolLocalizationCreated = ToolLocalizationDumies.CreateOneToolLocalization();
            var toolLocalizationService = new ToolLocalizationService(new FakeToolLocalizationRepository());
            toolLocalizationService.CreateToolLocalization(toolLocalizationCreated);

            var toolLocalizationFounded = toolLocalizationService.FindToolLocalizationByName(toolLocalizationCreated.Name);

            Assert.AreEqual(toolLocalizationFounded.Name, toolLocalizationCreated.Name);
        }

        [TestMethod]
        public void Should_Link_One_ToolLocalization_To_Company()
        {
            var toolsLocalizationsId = new List<int>();
            var company = CompanyDumies.CreateOneCompany();
            var toolLocalization = ToolLocalizationDumies.CreateOneToolLocalization();
            toolsLocalizationsId.Add(toolLocalization.Id);
            var toolLocalizationService = new ToolLocalizationService(new FakeToolLocalizationRepository());
            var companyService = new CompanyService(new FakeCompanyRepository(), new StubUnitOfWork());
            toolLocalizationService.CreateToolLocalization(toolLocalization);
            companyService.CreateCompany(company);

            toolLocalizationService.LinkToolsLocalizationToCompany(toolsLocalizationsId, company);

            Assert.IsNotNull(companyService.FindCompany(company.Id).ToolsLocalizations.FirstOrDefault(tl => tl.Id == toolLocalization.Id));
        }

    }

    public class FakeToolLocalizationRepository : IToolLocalizationRepository
    {
        private readonly ICollection<ToolLocalization> _toolLocalizations = new Collection<ToolLocalization>();


        public void Add(ToolLocalization toolLocalization)
        {
            _toolLocalizations.Add(toolLocalization);
        }

        public void Remove(object entitie)
        {
            var toolLocalizationToDelete = _toolLocalizations.FirstOrDefault(t => t.Id == (int)entitie);

            _toolLocalizations.Remove(toolLocalizationToDelete);
        }

        public void Update(ToolLocalization toolLocalization)
        {
            var toolLocalizationToUpdate = _toolLocalizations.FirstOrDefault(t => t.Id == toolLocalization.Id);

            toolLocalizationToUpdate.Name = toolLocalizationToUpdate.Name;
            toolLocalizationToUpdate.Company = toolLocalization.Company;
        }

        public IEnumerable<ToolLocalization> GetAll()
        {
            return _toolLocalizations;
        }

        public IEnumerable<ToolLocalization> Query(Func<ToolLocalization, bool> filter)
        {
            throw new NotImplementedException();
        }

        public ToolLocalization GetById(object id)
        {
            return _toolLocalizations.FirstOrDefault(t => t.Id == (int)id);
        }

        public ToolLocalization Get(Expression<Func<ToolLocalization, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public ToolLocalization GetByName(string name)
        {
            return _toolLocalizations.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
