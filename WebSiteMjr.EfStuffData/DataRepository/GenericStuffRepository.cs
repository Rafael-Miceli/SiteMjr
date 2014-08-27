using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfBaseData.DataRepository;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public abstract class GenericStuffRepository<TEntity, TId>: GenericRepository<TEntity, StuffContext, TId> where TEntity: Key<TId>
    {
        protected GenericStuffRepository(UnitOfWork<StuffContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
