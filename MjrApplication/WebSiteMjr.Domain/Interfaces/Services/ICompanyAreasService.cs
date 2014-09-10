using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ICompanyAreasService
    {
        void CreateCompanyArea(CompanyArea companyArea);
        void UpdateCompanyArea(CompanyArea companyArea);
        CompanyArea FindCompanyArea(object companyAreaId);
        void DeleteCompanyArea(object id);
        CompanyArea FindCompanyAreaByName(string name);
        void LinkToolsLocalizationToCompany(List<int> companyAreasId, Company company);
        IEnumerable<CompanyArea> ListCompanyAreas();
    }
}