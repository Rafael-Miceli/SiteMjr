using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ICompanyAreasService
    {
        void CreateToolLocalization(CompanyArea companyArea);
        void UpdateToolLocalization(CompanyArea companyArea);
        CompanyArea FindToolLocalization(object toolLocalizationId);
        void DeleteToolLocalization(object id);
        CompanyArea FindToolLocalizationByName(string name);
        void LinkToolsLocalizationToCompany(List<int> toolsLocalizationsId, Company company);
        IEnumerable<CompanyArea> ListToolsLocalizations();
    }
}