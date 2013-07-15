using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebSiteMjr.Domain.Interfaces.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entitie);
        void Remove(object entitie);
        void Update(T entitie);
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Func<T, bool> filter);
        T GetById(object identitie);
        T Get(Expression<Func<T, bool>> filter);
        //void Save();
    }
}
