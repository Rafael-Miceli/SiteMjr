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
    public abstract class GenericRepository<TEntity>: IGenericRepository<TEntity>, IDisposable where TEntity:IntId
    {
        private readonly PersonsContext _context;

        protected GenericRepository()
        {
            _context = new PersonsUow().Context;
        }


        protected GenericRepository(IUnitOfWork<PersonsContext> uow)
        {
            _context = uow.Context;
        }     

        public void Add(TEntity entitie)
        {
            //In Entity Framework the Add() Method Adds all of the graphs related to the context
            //_context.Set<TEntity>().Add(entitie);

            //But in the Entry() it only changes the first entitie 
            _context.Entry(entitie).State = EntityState.Added;
        }

        public void Remove(object entitie)
        {
            var entitieToRemove = _context.Set<TEntity>().Find(entitie);
            _context.Set<TEntity>().Remove(entitieToRemove);
        }

        public void Update(TEntity entitie)
        {
            //var entry = _context.Entry(entitie);

            //if (entry.State == EntityState.Detached) return;

            var attachedEntity = _context.Set<TEntity>().Find(entitie.Id);

            if (attachedEntity != null)
                _context.Entry(attachedEntity).CurrentValues.SetValues(entitie);
            else
                _context.Entry(entitie).State = EntityState.Modified;

            //_context.ApplyStateChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Query(Func<TEntity, bool> filter)
        {
            return _context.Set<TEntity>().Where(filter);
        }

        public TEntity GetById(object idEntitie)
        {
            return _context.Set<TEntity>().Find(idEntitie);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}
