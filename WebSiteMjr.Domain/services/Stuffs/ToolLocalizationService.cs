using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class ToolLocalizationService : IToolLocalizationService
    {
        private readonly IToolLocalizationRepository _toolLocalizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToolLocalizationService(IToolLocalizationRepository toolLocalizationRepository, IUnitOfWork unitOfWork)
        {
            _toolLocalizationRepository = toolLocalizationRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateToolLocalization(ToolLocalization toolLocalization)
        {
            _toolLocalizationRepository.Add(toolLocalization);
            _unitOfWork.Save();
        }

        public void UpdateToolLocalization(ToolLocalization toolLocalization)
        {
            _toolLocalizationRepository.Update(toolLocalization);
            _unitOfWork.Save();
        }

        public ToolLocalization FindToolLocalization(object toolLocalizationId)
        {
            return _toolLocalizationRepository.GetById(toolLocalizationId);
        }

        public void DeleteToolLocalization(object id)
        {
            _toolLocalizationRepository.Remove(id);
            _unitOfWork.Save();
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

            _unitOfWork.Save();
        }

        public IEnumerable<ToolLocalization> ListToolsLocalizations()
        {
            return _toolLocalizationRepository.GetAll();
        }
    }
}