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

        public void CreateCompany(Company user)
        {
            _companyRepository.Add(user);
            _companyRepository.Save();
        }

        public void UpdateCompany(Company user)
        {
            _companyRepository.Update(user);
            _companyRepository.Save();
        }

        public void DeleteCompany(object user)
        {
            _companyRepository.Remove(user);
            _companyRepository.Save();
        }

        public IEnumerable<Company> ListCompany()
        {
            return _companyRepository.GetAll();
        }

        public Company FindCompany(object iduser)
        {
            return _companyRepository.GetById(iduser);
        }
    }
}
