using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Controllers.Api
{
    public class CompanyApiController : ApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyApiController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public IEnumerable<string> ListCompanyAreas(string companyName)
        {
            return _companyService.FindCompanyCompanyAreasNames(companyName).ToArray();
        }
    }
}