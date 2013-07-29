using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class StuffRepository: GenericStuffRepository<Stuff>, IStuffRepository
    {
        public StuffRepository(UnitOfWork<StuffContext> uow) : base(uow)
        {}

        public void AddGraph(Stuff stuff)
        {
            Context.Set<Stuff>().Add(stuff);
        }
    }
}
