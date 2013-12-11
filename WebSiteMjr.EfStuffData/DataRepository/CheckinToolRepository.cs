using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class CheckinToolRepository: GenericStuffRepository<CheckinTool>, ICheckinToolRepository
    {
        public CheckinToolRepository(UnitOfWork<StuffContext> uow) : base(uow)
        {}
    }
}
