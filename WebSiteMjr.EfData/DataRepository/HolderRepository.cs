using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class HolderRepository : GenericPersonRepository<Holder>, IHolderRepository
    {
        public HolderRepository(UnitOfWork<PersonsContext> uow)
            : base(uow)
        { }
        public Holder GetHolderByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<Holder> GetAllHoldersNotDeleted()
        {
            var holders = GetAll().ToList();
            return holders.Any() ? holders.Where(h => !h.IsDeleted): null;
        }
    }
}