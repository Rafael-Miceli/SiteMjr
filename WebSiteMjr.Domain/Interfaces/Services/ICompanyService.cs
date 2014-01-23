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
        Company FindCompany(object idcompany);
        Company FindCompanyByName(string companyName);
        ICollection<CompanyArea> FindCompanyCompanyAreas(string name);
    }
}