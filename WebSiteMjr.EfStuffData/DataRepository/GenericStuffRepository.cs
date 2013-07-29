using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfBaseData.DataRepository;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public abstract class GenericStuffRepository<TEntity>: GenericRepository<TEntity, StuffContext> where TEntity: IntId 
    {
        protected GenericStuffRepository(UnitOfWork<StuffContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
