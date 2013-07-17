using WebSiteMjr.EfData.UnitOfWork;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
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
