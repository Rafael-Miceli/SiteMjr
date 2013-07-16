using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCompany(Company company)
        {
            _companyRepository.Add(company);
            _unitOfWork.Save();
        }

        public void UpdateCompany(Company company)
        {
            _companyRepository.Update(company);
            _unitOfWork.Save();
        }

        public void DeleteCompany(object company)
        {
            _companyRepository.Remove(company);
            _unitOfWork.Save();
        }

        public IEnumerable<Company> ListCompany()
        {
            return _companyRepository.GetAll();
        }

        public virtual Company FindCompany(object idcompany)
        {
            return _companyRepository.GetById(idcompany);
        }

    }
}
