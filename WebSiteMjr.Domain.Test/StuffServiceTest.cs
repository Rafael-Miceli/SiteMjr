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
            StuffMovimentService stuffMovimentService = new StuffMovimentService();
            var listOfStuffMoviments = stuffMovimentService.GetStuffMoviments(stuff);

            var stuffMoviment = listOfStuffMoviments.lastEntry();

            Assert.AreEqual(holder.Name, stuffMoviment.Holder.Name);
        }
    }
}
