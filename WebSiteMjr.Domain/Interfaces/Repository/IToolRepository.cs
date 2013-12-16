using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IToolRepository: IGenericRepository<Tool>
    {
        void AddOrUpdateGraph(Tool stuff);
        void UpdateGraph(Tool stuff);
        Tool GetToolByName(string name);
    }
}
