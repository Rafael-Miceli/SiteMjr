using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfBaseData.DataRepository;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericPersonRepository<TEntity>: GenericRepository<TEntity, PersonsContext> where TEntity: IntId 
    {
        protected GenericPersonRepository(UnitOfWork<PersonsContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
