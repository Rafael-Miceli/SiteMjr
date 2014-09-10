using System;
using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class CompanyService : ICompanyService
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

        public Company FindCompanyByName(string companyName)
        {
            return _companyRepository.GetCompanyByName(companyName);
        }

        public ICollection<CompanyArea> FindCompanyCompanyAreas(string companyName)
        {
            var company = FindCompanyByName(companyName);
            return company != null ? company.CompanyAreas : null;
        }

        public IEnumerable<string> FindCompanyCompanyAreasNames(string companyName)
        {
            var companyAreas = FindCompanyCompanyAreas(companyName);
            return companyAreas != null ? FindCompanyCompanyAreas(companyName).Select(ca => ca.Name) : new List<string>() ;
        }

        public IEnumerable<Company> ListCompaniesNotDeleted()
        {
            return _companyRepository.GetAllCompaniesNotDeleted();
        }

        public Company ExistsCheckinOfToolInCompany(int employeeCompanyHolderId)
        {
            try
            {
                return FindCompany(employeeCompanyHolderId);
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
