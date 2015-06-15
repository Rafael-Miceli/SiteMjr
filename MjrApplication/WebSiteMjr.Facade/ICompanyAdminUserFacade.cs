using System.Threading.Tasks;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Facade
{
    public interface ICompanyAdminUserFacade
    {
        Task CreateAdminUserForCompany(int companyId);
        Task CreateCompanyInSena(Company company);
        void CreateAdminUserForCompanyInSena(Company company);
    }
}