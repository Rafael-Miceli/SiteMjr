using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class CompanyAreasService : ICompanyAreasService
    {
        private readonly IToolLocalizationRepository _toolLocalizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyAreasService(IToolLocalizationRepository toolLocalizationRepository, IUnitOfWork unitOfWork)
        {
            _toolLocalizationRepository = toolLocalizationRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateToolLocalization(CompanyArea companyArea)
        {
            if (ToolLocalizationExists(companyArea)) throw new ObjectExistsException<CompanyArea>();

            _toolLocalizationRepository.Add(companyArea);
            _unitOfWork.Save();
        }

        private bool ToolLocalizationExists(CompanyArea companyArea)
        {
            return FindToolLocalizationByName(companyArea.Name) != null;
        }

        public void UpdateToolLocalization(CompanyArea companyArea)
        {
            if (ToolLocalizationExists(companyArea)) throw new ObjectExistsException<CompanyArea>();

            _toolLocalizationRepository.Update(companyArea);
            _unitOfWork.Save();
        }

        public CompanyArea FindToolLocalization(object toolLocalizationId)
        {
            return _toolLocalizationRepository.GetById(toolLocalizationId);
        }

        public void DeleteToolLocalization(object id)
        {
            _toolLocalizationRepository.Remove(id);
            _unitOfWork.Save();
        }
        
        public CompanyArea FindToolLocalizationByName(string name)
        {
            return _toolLocalizationRepository.GetByName(name);
        }

        public void LinkToolsLocalizationToCompany(List<int> toolsLocalizationsId, Company company)
        {
            var toolsLocalizations = _toolLocalizationRepository.GetAll().ToList();

            foreach (var toolsId in toolsLocalizationsId)
            {
                company.CompanyAreas.Add(toolsLocalizations.FirstOrDefault(tl => tl.Id == toolsId));
            }

            _unitOfWork.Save();
        }

        public IEnumerable<CompanyArea> ListToolsLocalizations()
        {
            return _toolLocalizationRepository.GetAll();
        }
    }
}