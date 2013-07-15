using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericPersonRepository<TEntity>: GenericRepository<TEntity, PersonsContext> where TEntity: IntId 
    {
        protected GenericPersonRepository(IUnitOfWork<PersonsContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
