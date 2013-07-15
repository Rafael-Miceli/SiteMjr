using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>, IDisposable
        where TEntity : IntId
        where TContext : BaseContext<TContext>
    {
        protected TContext Context;

        public void Add(TEntity entitie)
        {
            //In Entity Framework the Add() Method Adds all of the graphs related to the context
            //_context.Set<TEntity>().Add(entitie);

            //But in the Entry() it only changes the first entitie 
            Context.Entry(entitie).State = EntityState.Added;
        }

        public void Remove(object entitie)
        {
            var entitieToRemove = Context.Set<TEntity>().Find(entitie);
            Context.Set<TEntity>().Remove(entitieToRemove);
        }

        public void Update(TEntity entitie)
        {
            //var entry = _context.Entry(entitie);

            //if (entry.State == EntityState.Detached) return;

            var attachedEntity = Context.Set<TEntity>().Find(entitie.Id);

            if (attachedEntity != null)
                Context.Entry(attachedEntity).CurrentValues.SetValues(entitie);
            else
                Context.Entry(entitie).State = EntityState.Modified;

            //_context.ApplyStateChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Query(Func<TEntity, bool> filter)
        {
            return Context.Set<TEntity>().Where(filter);
        }

        public TEntity GetById(object idEntitie)
        {
            return Context.Set<TEntity>().Find(idEntitie);
        }

        //public void Save()
        //{
        //    Context.SaveChanges();
        //}

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().SingleOrDefault(filter);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
