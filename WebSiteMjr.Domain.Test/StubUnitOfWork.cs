using WebSiteMjr.Domain.Interfaces.Uow;

namespace WebSiteMjr.Domain.Test
{
    public class StubUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            
        }

        public int Save()
        {
            return 1;
        }
    }
}