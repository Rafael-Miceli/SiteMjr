using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels.Company;

namespace WebSiteMjr.Assembler
{
    public class CompanyMapper
    {
        private readonly ICompanyService _companyService;
        private readonly IToolLocalizationService _toolLocalizationService;

        public CompanyMapper(ICompanyService companyService, IToolLocalizationService toolLocalizationService)
        {
            _companyService = companyService;
            _toolLocalizationService = toolLocalizationService;
        }

        public ListCompanyViewModel CompanyToListCompanyViewModel()
        {
            return new ListCompanyViewModel
            {
                Companies = _companyService.ListCompany().ToList()
            };
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
                ToolsLocalizations = MapToolsLocalizationInCompanyToView(company.ToolsLocalizations)
            };
            return editCompanyViewModel;
        }

        public Company EditCompanyViewModelToCompany(EditCompanyViewModel editCompanyViewModel)
        {
            return null;
        }

        private IEnumerable<SelectListItem> MapToolsLocalizationInCompanyToView(ICollection<ToolLocalization> toolsLocalizationsInCompany)
        {
            var toolsLocalization = _toolLocalizationService.ListToolsLocalizations();
            var toolsLocalizationMapped = new List<SelectListItem>();

            foreach (var toolLocalization in toolsLocalization)
            {
                toolsLocalizationMapped.Add(new SelectListItem
                {
                    Selected = toolsLocalizationsInCompany.Any(t => t.Id == toolLocalization.Id),
                    Text = toolLocalization.Name,
                    Value = toolLocalization.Id.ToString()
                });
            }

            return toolsLocalizationMapped;
        }
    }
}
