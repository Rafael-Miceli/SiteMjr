using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfStuffData.Context;
using WebSiteMjr.EfStuffData.DataRepository;
using WebSiteMjr.EfStuffData.UnitOfWork;

namespace WebSiteMjr.EfStuffDataTest.IntegrationTest
{
    //[TestClass]
    //public class StuffRepositoryTest
    //{
    //    [TestMethod]
    //    public void Should_Edit_Stuff_With_No_Manufacturer()
    //    {
    //        var stuffUow = new StuffUow(new StuffContext());
    //        var stuffRepository = new StuffRepository(stuffUow);
    //        var stuffCategoryRepository = new StuffCategoryRepository(stuffUow);
    //        var stuff = stuffRepository.GetById(2);

    //        stuff.StuffManufacture = null;
    //        stuff.State = State.Modified;
    //        stuff.StuffCategory = stuffCategoryRepository.GetById(1);
    //        stuff.StuffCategory.State = State.Modified;

    //        stuffRepository.Update(stuff);
    //        stuffUow.Save();
    //    }
    //}
}
