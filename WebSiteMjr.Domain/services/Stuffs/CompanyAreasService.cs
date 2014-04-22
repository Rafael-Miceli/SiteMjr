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
        private readonly ICompanyAreaRepository _companyAreaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyAreasService(ICompanyAreaRepository companyAreaRepository, IUnitOfWork unitOfWork)
        {
            _companyAreaRepository = companyAreaRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCompanyArea(CompanyArea companyArea)
        {
            if (CompanyAreaExists(companyArea.Name)) throw new ObjectExistsException<CompanyArea>();

            _companyAreaRepository.Add(companyArea);
            _unitOfWork.Save();
        }

        private bool CompanyAreaExists(string companyAreaName)
        {
            return FindCompanyAreaByName(companyAreaName) != null;
        }

        public void UpdateCompanyArea(CompanyArea companyArea)
        {
            if (CompanyAreaExists(companyArea.Name)) throw new ObjectExistsException<CompanyArea>();

            _companyAreaRepository.Update(companyArea);
            _unitOfWork.Save();
        }

        public CompanyArea FindCompanyArea(object companyAreaId)
        {
            return _companyAreaRepository.GetById(companyAreaId);
        }

        public void DeleteCompanyArea(object id)
        {
            _companyAreaRepository.Remove(id);
            _unitOfWork.Save();
        }
        
        public CompanyArea FindCompanyAreaByName(string name)
        {
            return _companyAreaRepository.GetByName(name);
        }

        public void LinkToolsLocalizationToCompany(List<int> companyAreasId, Company company)
        {
            var companyAreas = _companyAreaRepository.GetAll().ToList();

            foreach (var toolsId in companyAreasId)
            {
                company.CompanyAreas.Add(companyAreas.FirstOrDefault(tl => tl.Id == toolsId));
            }

            _unitOfWork.Save();
        }

        public IEnumerable<CompanyArea> ListCompanyAreas()
        {
            return _companyAreaRepository.GetAll();
        }
    }
}