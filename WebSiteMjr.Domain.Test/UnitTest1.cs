using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class StuffServiceTest
    {
        [TestMethod]
        public void Should_Find_With_Whom_Is_The_Stuff()
        {
            StuffService stuff = new StuffService();
            var listOfFoundedStuffs = stuff.FindStuffs(listIdOfStuffs);



            Assert.AreEqual("Celso", employeeNameWithTheStuff);
        }
    }
}
