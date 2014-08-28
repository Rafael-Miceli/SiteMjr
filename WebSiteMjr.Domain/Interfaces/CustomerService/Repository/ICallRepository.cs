using System.Collections.Generic;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;

namespace WebSiteMjr.Domain.Interfaces.CustomerService.Repository
{
    public interface ICallRepository
    {
        void Add(Call call);
        void Update(Call call);
        IEnumerable<Call> GetAll();
        Call FindEntity(object entityId);
    }
}