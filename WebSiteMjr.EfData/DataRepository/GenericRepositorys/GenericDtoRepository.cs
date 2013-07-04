using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.DataRepository.GenericRepositorys
{
    public class GenericDtoGenericRepository<TEntity, TUEntity> : IGenericRepository<TUEntity>, IDisposable where TUEntity : class where TEntity: class 
    {

        private readonly PersonsContext _context;        
        private readonly DbSet<TEntity> _dbSet;
        private TEntity _entity;

        public GenericDtoGenericRepository(PersonsContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public GenericDtoGenericRepository()
        {
            _context = new PersonsContext();
        }

        public IEnumerable<TUEntity> Query(Func<TUEntity, bool> filter)
        {
            //Todo think here, instead of use AutoMapper, you could also use a contract between DTOs and Entities. Anyway take a look at this link http://www.devtrends.co.uk/blog/stop-using-automapper-in-your-data-access-code

            var mapper = Mapper.CreateMapExpression<TEntity, TUEntity>().Compile();
            Expression<Func<TEntity, bool>> mappedSelector = entity => filter(mapper(entity));
            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TUEntity>>(_context.Set<TEntity>().Where(mappedSelector));
        }

        public TUEntity Get(Expression<Func<TUEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update(TUEntity entityToUpdate)
        {
            _context.Entry(Mapper.Map<TUEntity, TEntity>(entityToUpdate)).State = EntityState.Modified;
        }

        public void Add(TUEntity entitieDto)
        {
            _context.Set<TEntity>().Add(Mapper.Map<TUEntity, TEntity>(entitieDto));
        }

        public void Remove(object entitieDto)
        {
            _entity = Mapper.Map<TUEntity, TEntity>(_context.Set<TUEntity>().Find(entitieDto));

            if (_context.Entry(_entity).State == EntityState.Detached)
            {
                _dbSet.Attach(_entity);
            }
            _dbSet.Remove(_entity);
        }

        public IEnumerable<TUEntity> GetAll()
        {
            return Mapper.Map<DbSet<TEntity>, DbSet<TUEntity>>(_context.Set<TEntity>());
        }

        public TUEntity GetById(object identitieDto)
        {
            return Mapper.Map<TEntity, TUEntity>(_context.Set<TEntity>().Find(identitieDto));
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
