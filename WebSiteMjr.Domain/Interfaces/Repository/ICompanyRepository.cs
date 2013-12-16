using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface ICompanyRepository: IGenericRepository<Company>
    {
        Company GetCompanyByName(string name);
    }
}