using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfBaseData.DataRepository;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericPersonRepository<TEntity, TId>: GenericRepository<TEntity, PersonsContext, TId> where TEntity: Key<TId>
    {
        protected GenericPersonRepository(UnitOfWork<PersonsContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
