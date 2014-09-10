using System;

namespace WebSiteMjr.Domain.Interfaces.Uow
{
    public interface IUnitOfWork: IDisposable
    {
        int Save();
    }
}
