using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.Helpers;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class StuffRepository: GenericStuffRepository<Stuff>, IStuffRepository
    {
        public StuffRepository(UnitOfWork<StuffContext> uow) : base(uow)
        {}

        public void AddOrUpdateGraph(Stuff stuff)
        {
            if (stuff.State == State.Added)
                Context.Set<Stuff>().Add(stuff);
            else
            {
                Context.Set<Stuff>().Add(stuff);
                Context.ApplyStateChanges();
            }
            
        }

        public void UpdateGraph(Stuff stuff)
        {
            Context.Set<Stuff>().Add(stuff);
            Context.ApplyStateChanges();
        }
    }
}
