using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;

namespace WebSiteMjr.Tests.Controllers
{
    [TestClass]
    public class StuffControllerTest
    {
        [TestMethod]
        [Ignore]
        public void Should_Create_Stuff_Category()
        {
            var expectedCategory = new StuffCategory {Id = 2, Name = "categoria"};

            var controller = new StuffController(null, new FakeStuffCategoryService(), null);
            var result = controller.CreateStuffCategory(expectedCategory);

            Assert.AreEqual(result.Data, expectedCategory.Name);
        }

        public class FakeStuffCategoryService : IStuffCategoryService
        {

            public void CreateStuffCategory(StuffCategory stuffCategory)
            {
                Console.WriteLine("Stuff Created");
            }

            public new void UpdateStuffCategory(StuffCategory stuffCategory)
            {
                throw new NotImplementedException();
            }

            public new void DeleteStuffCategory(object stuffCategory)
            {
                throw new NotImplementedException();
            }

            public new IEnumerable<StuffCategory> ListStuffCategory()
            {
                throw new NotImplementedException();
            }

            public new StuffCategory FindStuffCategory(object idStuffCategory)
            {
                throw new NotImplementedException();
            }

            public new StuffCategory FindStuffCategoryByName(string name)
            {
                return new StuffCategory {Id = 1, Name = name};
            }
        }
    }
}
