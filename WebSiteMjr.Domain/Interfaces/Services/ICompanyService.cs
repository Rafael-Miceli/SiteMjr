using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(object company);
        IEnumerable<Company> ListCompany();
        IEnumerable<Company> ListCompaniesNotDeleted();
        Company FindCompany(object idcompany);
        Company FindCompanyByName(string companyName);
        ICollection<CompanyArea> FindCompanyCompanyAreas(string companyName);
        IEnumerable<string> FindCompanyCompanyAreasNames(string companyName);
        Company ExistsCheckinOfToolInCompany(int employeeCompanyHolderId);
    }
}