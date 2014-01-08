using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebSiteMjr.Domain.Interfaces.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entitie);
        void Remove(object entitieId);
        void Update(T entitie);
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Func<T, bool> filter);
        T GetById(object entitieId);
        T Get(Expression<Func<T, bool>> filter);
        //void Save();
    }
}
