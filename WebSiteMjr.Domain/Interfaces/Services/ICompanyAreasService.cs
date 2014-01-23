using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ICompanyAreasService
    {
        void CreateCompanyArea(CompanyArea companyArea);
        void UpdateCompanyArea(CompanyArea companyArea);
        CompanyArea FindCompanyArea(object CompanyAreaId);
        void DeleteCompanyArea(object id);
        CompanyArea FindCompanyAreaByName(string name);
        void LinkToolsLocalizationToCompany(List<int> CompanyAreasId, Company company);
        IEnumerable<CompanyArea> ListCompanyAreas();
    }
}