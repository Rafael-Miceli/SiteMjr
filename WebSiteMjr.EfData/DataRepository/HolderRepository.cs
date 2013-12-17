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
    }
}