using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfStuffData.Context;

namespace WebSiteMjr.EfStuffData.DataRepository
{
    public class StuffManufactureRepository: GenericStuffRepository<StuffManufacture, int>, IStuffManufactureRepository
    {
        public StuffManufactureRepository(UnitOfWork<StuffContext> uow): base(uow)
        {}

        public StuffManufacture GetStuffManufacturerByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }
    }
}
