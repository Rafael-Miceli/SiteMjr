using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface ICompanyAreaRepository : IGenericRepository<CompanyArea>
    {
        CompanyArea GetByName(string name);
    }
}