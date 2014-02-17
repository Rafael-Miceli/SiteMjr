using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Controllers.Api
{
    public class HolderApiController : ApiController
    {
        private readonly IHolderService _holderService;

        public HolderApiController(IHolderService holderService)
        {
            _holderService = holderService;
        }

        // GET api/<controller>
        public IEnumerable<string> GetEmployeeCompanyHoldersName()
        {
            return _holderService.ListEmployeeCompanyHolderName().ToArray();
        }

        public IEnumerable<string> GetNotDeletedHoldersName()
        {
            return _holderService.ListNotDeletedHolderName().ToArray();
        }

        
    }
}