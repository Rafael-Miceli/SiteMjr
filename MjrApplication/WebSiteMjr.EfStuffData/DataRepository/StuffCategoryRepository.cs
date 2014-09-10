using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class StuffCategoryRepository: GenericStuffRepository<StuffCategory, int>, IStuffCategoryRepository
    {
        public StuffCategoryRepository(UnitOfWork<StuffContext> uow): base(uow)
        {}

        public StuffCategory GetStuffCategoryByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }
    }
}
