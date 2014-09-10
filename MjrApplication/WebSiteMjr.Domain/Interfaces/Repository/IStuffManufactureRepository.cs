using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IStuffManufactureRepository : IGenericRepository<StuffManufacture>
    {
        StuffManufacture GetStuffManufacturerByName(string name);
    }
}
