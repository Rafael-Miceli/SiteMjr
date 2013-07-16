using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericStuffRepository<TEntity>: GenericRepository<TEntity, StuffContext> where TEntity: IntId 
    {
        protected GenericStuffRepository(UnitOfWork<StuffContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
