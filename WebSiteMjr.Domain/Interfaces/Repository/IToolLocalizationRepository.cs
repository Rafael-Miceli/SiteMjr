using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IToolLocalizationRepository
    {
        void Add(ToolLocalization toolLocalization);
        void Update(ToolLocalization toolLocalization);
        ToolLocalization GetById(object id);
        void Delete(object id);
        ToolLocalization GetByName(string name);
    }
}