using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.UnitOfWork
{
    public class StuffUow : IUnitOfWork<StuffContext>
    {
        private readonly StuffContext _context;

        public StuffUow()
        {
            _context = new StuffContext();
        }

        public StuffUow(StuffContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public StuffContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
