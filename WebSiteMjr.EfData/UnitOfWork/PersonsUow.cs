using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.UnitOfWork
{
    public class PersonsUow:IUnitOfWork<PersonsContext>
    {
        private readonly PersonsContext _context;

        public PersonsUow()
        {
            _context = new PersonsContext();
        }

        public PersonsUow(PersonsContext context)
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

        public PersonsContext Context 
        { 
            get { return _context; } 
        }
    }
}
