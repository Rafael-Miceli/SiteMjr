using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void CreateCompany(Company company)
        {
            _companyRepository.Add(company);
            _companyRepository.Save();
        }

        public void UpdateCompany(Company company)
        {
            _companyRepository.Update(company);
            _companyRepository.Save();
        }

        public void DeleteCompany(object company)
        {
            _companyRepository.Remove(company);
            _companyRepository.Save();
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
