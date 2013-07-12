using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericPersonRepository<TEntity>: GenericRepository<TEntity, PersonsContext> where TEntity: IntId // IGenericRepository<TEntity>, IDisposable where TEntity:IntId
    {
        protected GenericPersonRepository()
        {
            Context = new PersonsUow().Context;
        }
        protected GenericPersonRepository(IUnitOfWork<PersonsContext> uow)
        {
            Context = uow.Context;
        }     
    }
}
