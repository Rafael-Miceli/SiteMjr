using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class StuffCategoryServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<StuffCategory>))]
        public void Should_Not_Register_Category_With_Same_Name()
        {
            var category = new StuffCategory
            {
                Id = 1,
                Name = "Categoria 1"
            };

            var mock = new Mock<IStuffCategoryService>();
            mock.Setup(e => e.CreateStuffCategory(category)).Throws<ObjectExistsException<StuffCategory>>();

            mock.Object.CreateStuffCategory(category);
            
        }

    }
}
