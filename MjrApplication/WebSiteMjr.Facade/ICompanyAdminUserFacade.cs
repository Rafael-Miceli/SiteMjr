using System.Threading.Tasks;

namespace WebSiteMjr.Facade
{
    public interface ICompanyAdminUserFacade
    {
        Task CreateAdminUserForCompany(int companyId);
    }
}