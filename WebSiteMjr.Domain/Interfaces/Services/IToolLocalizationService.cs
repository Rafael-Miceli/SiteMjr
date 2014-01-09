using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IToolLocalizationService
    {
        void CreateToolLocalization(ToolLocalization toolLocalization);
        void UpdateToolLocalization(ToolLocalization toolLocalization);
        ToolLocalization FindToolLocalization(object toolLocalizationId);
        void DeleteToolLocalization(object id);
        ToolLocalization FindToolLocalizationByName(string name);
        void LinkToolsLocalizationToCompany(List<int> toolsLocalizationsId, Company company);
        IEnumerable<ToolLocalization> ListToolsLocalizations();
    }
}