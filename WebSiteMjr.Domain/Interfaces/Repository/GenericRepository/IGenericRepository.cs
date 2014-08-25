using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
}
