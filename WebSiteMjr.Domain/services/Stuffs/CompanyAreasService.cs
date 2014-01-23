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
        private readonly ICompanyAreaRepository _CompanyAreaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyAreasService(ICompanyAreaRepository CompanyAreaRepository, IUnitOfWork unitOfWork)
        {
            _CompanyAreaRepository = CompanyAreaRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCompanyArea(CompanyArea companyArea)
        {
            if (CompanyAreaExists(companyArea)) throw new ObjectExistsException<CompanyArea>();

            _CompanyAreaRepository.Add(companyArea);
            _unitOfWork.Save();
        }

        private bool CompanyAreaExists(CompanyArea companyArea)
        {
            return FindCompanyAreaByName(companyArea.Name) != null;
        }

        public void UpdateCompanyArea(CompanyArea companyArea)
        {
            if (CompanyAreaExists(companyArea)) throw new ObjectExistsException<CompanyArea>();

            _CompanyAreaRepository.Update(companyArea);
            _unitOfWork.Save();
        }

        public CompanyArea FindCompanyArea(object CompanyAreaId)
        {
            return _CompanyAreaRepository.GetById(CompanyAreaId);
        }

        public void DeleteCompanyArea(object id)
        {
            _CompanyAreaRepository.Remove(id);
            _unitOfWork.Save();
        }
        
        public CompanyArea FindCompanyAreaByName(string name)
        {
            return _CompanyAreaRepository.GetByName(name);
        }

        public void LinkToolsLocalizationToCompany(List<int> CompanyAreasId, Company company)
        {
            var CompanyAreas = _CompanyAreaRepository.GetAll().ToList();

            foreach (var toolsId in CompanyAreasId)
            {
                company.CompanyAreas.Add(CompanyAreas.FirstOrDefault(tl => tl.Id == toolsId));
            }

            _unitOfWork.Save();
        }

        public IEnumerable<CompanyArea> ListCompanyAreas()
        {
            return _CompanyAreaRepository.GetAll();
        }
    }
}