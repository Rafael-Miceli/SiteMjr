using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebSiteMjr.Domain.Interfaces.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entity);
        void Remove(object entityId);
        void DeleteEntityPermanently(T entityToRemove);
        bool ImplementsIsDeletable(T entityToRemove);
        void MakeEntityDeleted(object entityId, T entityToRemove);
        void Update(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Func<T, bool> filter);
        T FindEntity(object entityId);
        T GetById(object entityId);
        T Get(Expression<Func<T, bool>> filter);
    }

    public abstract class GenericRepositoryTemplate<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity)
        {
            
        }

        public void Remove(object entityId)
        {
            var entitieToRemove = FindEntity(entityId);
            if (ImplementsIsDeletable(entitieToRemove))
            {
                MakeEntityDeleted(entityId, entitieToRemove);
            }
            else
                DeleteEntityPermanently(entitieToRemove);
        }

        public void DeleteEntityPermanently(TEntity entityToRemove)
        {
            
        }

        public bool ImplementsIsDeletable(TEntity entityToRemove)
        {
            return false;
        }

        public void MakeEntityDeleted(object entitie, TEntity entitieToRemove)
        {
            
        }

        public void Update(TEntity entitie)
        {
            
        }

        public IEnumerable<TEntity> GetAll()
        {
            return null;
        }

        public IEnumerable<TEntity> Query(Func<TEntity, bool> filter)
        {
            return null;
        }

        public TEntity FindEntity(object entityId)
        {
            return null;
        }

        public TEntity GetById(object entitieId)
        {
            return null;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return null;
        }
    }
}
