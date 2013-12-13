using System.Linq;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class CompanyMapper
    {
        private readonly ICompanyService _companyService;

        public CompanyMapper(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public ListCompanyViewModel CompanyToListCompanyViewModel()
        {
            return new ListCompanyViewModel
            {
                Companies = _companyService.ListCompany().ToList()
            };
        }
    }
}
