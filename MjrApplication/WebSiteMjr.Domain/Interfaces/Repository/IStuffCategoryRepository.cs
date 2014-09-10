using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IStuffCategoryRepository : IGenericRepository<StuffCategory>
    {
        StuffCategory GetStuffCategoryByName(string name);
    }
}