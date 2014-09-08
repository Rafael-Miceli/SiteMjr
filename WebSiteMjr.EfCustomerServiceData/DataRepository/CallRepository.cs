using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.CustomerService.Repository;
using WebSiteMjr.EfBaseData.Context;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfCustomerServiceData.Context;

namespace WebSiteMjr.EfCustomerServiceData.DataRepository
{
    public class CallRepository: ICallRepository, IDisposable
    {
        private readonly BaseContext<CustomerServiceContext> _context;

        public CallRepository(UnitOfWork<CustomerServiceContext> uow)
        {
            _context = uow.Context;
        }

        public void Add(Call call)
        {
            _context.Entry(call).State = EntityState.Added;
        }

        public void Update(Call call)
        {
            var attachedEntity = FindEntity(call.Id);

            if (attachedEntity != null)
                _context.Entry(attachedEntity).CurrentValues.SetValues(call);
            else
                _context.Entry(call).State = EntityState.Modified;
        }

        public Call FindEntity(object entityId)
        {
            return _context.Set<Call>().Find(entityId);
        }

        public IEnumerable<Call> GetAll()
        {
            return _context.Set<Call>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
