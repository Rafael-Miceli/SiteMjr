using System.Data.Entity;
using WebSiteMjr.Domain.Interfaces.Uow;

namespace WebSiteMjr.EfBaseData.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext: DbContext, new()
    {
        private readonly TContext _context;
        public TContext Context
        {
            get
            {
                return _context;
            }
        }

        public UnitOfWork()
        {
            _context = new TContext();
        }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
