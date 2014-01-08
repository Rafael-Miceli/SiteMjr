using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class ToolLocalizationService
    {
        private readonly IToolLocalizationRepository _toolLocalizationRepository;

        public ToolLocalizationService(IToolLocalizationRepository toolLocalizationRepository)
        {
            _toolLocalizationRepository = toolLocalizationRepository;
        }

        public void CreateToolLocalization(ToolLocalization toolLocalization)
        {
            _toolLocalizationRepository.Add(toolLocalization);
        }

        public void UpdateToolLocalization(ToolLocalization toolLocalization)
        {
            _toolLocalizationRepository.Update(toolLocalization);
        }

        public ToolLocalization FindToolLocalization(object toolLocalizationId)
        {
            return _toolLocalizationRepository.GetById(toolLocalizationId);
        }

        public void DeleteToolLocalization(object id)
        {
            _toolLocalizationRepository.Remove(id);
        }
        
        public ToolLocalization FindToolLocalizationByName(string name)
        {
            return _toolLocalizationRepository.GetByName(name);
        }

        public void LinkToolsLocalizationToCompany(List<int> toolsLocalizationsId, Company company)
        {
            var toolsLocalizations = _toolLocalizationRepository.GetAll().ToList();

            foreach (var toolsId in toolsLocalizationsId)
            {
                company.ToolsLocalizations.Add(toolsLocalizations.FirstOrDefault(tl => tl.Id == toolsId));
            }
        }
    }
}