using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfBaseData.DataRepository
{
    public abstract class GenericRepository<TEntity, TContext, TId> : IGenericRepository<TEntity>, IDisposable
        where TEntity : Key<TId>
        where TContext : BaseContext<TContext>
    {
        protected TContext Context;

        public void Add(TEntity entitie)
        {
            Context.Entry(entitie).State = EntityState.Added;
        }

        public void Remove(object entityId)
        {
            var entityToRemove = FindEntity(entityId);

            if (entityToRemove == null)
                return;
                
            if (ImplementsIsDeletable(entityToRemove))
            {
                MakeEntityDeleted(entityId, entityToRemove);
            }
            else
                DeleteEntityPermanently(entityToRemove);
        }

        public TEntity FindEntity(object entityId)
        {
            return Context.Set<TEntity>().Find(entityId);
        }

        public void DeleteEntityPermanently(TEntity entitieToRemove)
        {
            Context.Set<TEntity>().Remove(entitieToRemove);
        }

        public bool ImplementsIsDeletable(TEntity entityToRemove)
        {
            return entityToRemove is INotDeletable;
        }

        public void MakeEntityDeleted(object entitie, TEntity entityToRemove)
        {
            var entityToUpdate = (entityToRemove as INotDeletable);

            if (!entityToUpdate.IsDeleted)
            {
                entityToUpdate.Name += " (Excluido)";
                entityToUpdate.IsDeleted = true;    
            }

            Context.Entry(entityToUpdate).CurrentValues.SetValues(entitie);
        }

        public void Update(TEntity entitie)
        {
            //var entry = _context.Entry(entitie);

            //if (entry.State == EntityState.Detached) return;

            var attachedEntity = FindEntity(entitie.Id);

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
            return FindEntity(idEntitie);
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
