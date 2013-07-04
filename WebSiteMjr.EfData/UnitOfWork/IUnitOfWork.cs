using System;
using System.Data.Entity;

namespace WebSiteMjr.EfData.UnitOfWork
{
    public interface IUnitOfWork<out TContext>: IDisposable where TContext: DbContext
    {
        int Save();
        TContext Context { get; }
    }
}
