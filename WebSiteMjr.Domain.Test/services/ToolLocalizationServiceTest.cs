using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
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
        public void Should_Link_ToolLocalization_To_Company()
        {
            
        }

    }

    public class FakeToolLocalizationRepository : IToolLocalizationRepository
    {
        private readonly List<ToolLocalization> _toolLocalizations = new List<ToolLocalization>();


        public void Add(ToolLocalization toolLocalization)
        {
            _toolLocalizations.Add(toolLocalization);
        }

        public void Update(ToolLocalization toolLocalization)
        {
            var toolLocalizationToUpdate = _toolLocalizations.Find(t => t.Id == toolLocalization.Id);

            toolLocalizationToUpdate.Name = toolLocalizationToUpdate.Name;
            toolLocalizationToUpdate.Company = toolLocalization.Company;
        }

        public ToolLocalization GetById(object id)
        {
            return _toolLocalizations.Find(t => t.Id == (int)id);
        }

        public void Delete(object id)
        {
            var toolLocalizationToDelete = _toolLocalizations.Find(t => t.Id == (int)id);

            _toolLocalizations.Remove(toolLocalizationToDelete);
        }

        public ToolLocalization GetByName(string name)
        {
            return _toolLocalizations.Find(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
