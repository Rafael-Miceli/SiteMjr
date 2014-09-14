using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels.CompaniesModel;

namespace WebSiteMjr.Assembler
{
    public class CompanyMapper
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyAreasService _companyAreasService;

        public CompanyMapper(ICompanyService companyService, ICompanyAreasService companyAreasService)
        {
            _companyService = companyService;
            _companyAreasService = companyAreasService;
        }

        public ListCompanyViewModel CompanyToListCompanyViewModel()
        {
            return new ListCompanyViewModel
            {
                Companies = _companyService.ListCompaniesNotDeleted().ToList()
            };
        }

        public Company EditCompanyViewModelToCompany(EditCompanyViewModel editCompanyViewModel)
        {
            var company = _companyService.FindCompany(editCompanyViewModel.Id);

            company.Name = editCompanyViewModel.Name;
            company.Email = editCompanyViewModel.Email;
            company.City = editCompanyViewModel.City;
            company.Address = editCompanyViewModel.Address;
            company.Phone = editCompanyViewModel.Phone;

            company.CompanyAreas = MapCompanyAreaSelectedInEditCompanyViewModelToCompany(editCompanyViewModel.CompanyAreas);

            return company;
        }
        
        public EditCompanyViewModel CompanyToEditCompanyViewModel(Company company)
        {
            var editCompanyViewModel = new EditCompanyViewModel
            {
                Id = company.Id,
                Address = company.Address,
                City = company.City,
                Email = company.Email,
                Name = company.Name,
                Phone = company.Phone,
                CompanyAreas = MapCompanyAreasInCompanyToView(company.CompanyAreas)
            };
            return editCompanyViewModel;
        }

        private IList<SelectListItem> MapCompanyAreasInCompanyToView(ICollection<CompanyArea> CompanyAreasInCompany)
        {
            var toolsLocalization = _companyAreasService.ListCompanyAreas();
            var toolsLocalizationMapped = new List<SelectListItem>();

            foreach (var CompanyArea in toolsLocalization)
            {
                toolsLocalizationMapped.Add(new SelectListItem
                {
                    Selected = CompanyAreasInCompany.Any(t => t.Id == CompanyArea.Id),
                    Text = CompanyArea.Name,
                    Value = CompanyArea.Id.ToString()
                });
            }

            return toolsLocalizationMapped;
        }

        private ICollection<CompanyArea> MapCompanyAreaSelectedInEditCompanyViewModelToCompany(IEnumerable<SelectListItem> companyAreasSelectedInView)
        {
            if (companyAreasSelectedInView == null) return null;
            var toolsLocalization = _companyAreasService.ListCompanyAreas().ToList();
            var toolsLocalizationMapped = new List<CompanyArea>();

            foreach (var CompanyAreaSelected in companyAreasSelectedInView)
            {
                if (CompanyAreaSelected.Selected)
                {
                    toolsLocalizationMapped.Add(toolsLocalization.FirstOrDefault(t => t.Id == int.Parse(CompanyAreaSelected.Value)));
                }
            }

            return toolsLocalizationMapped;
        }
    }
}
