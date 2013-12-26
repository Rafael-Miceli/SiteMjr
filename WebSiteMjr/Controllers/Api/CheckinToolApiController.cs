using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Controllers.Api
{
    public class CheckinToolApiController : ApiController
    {
        private readonly ICheckinToolService _checkinToolService;

        public CheckinToolApiController(ICheckinToolService checkinToolService)
        {
            _checkinToolService = checkinToolService;
        }

        // GET api/<controller>
        public IEnumerable<string> GetEmployeeCompanyHoldersName()
        {
            return _checkinToolService.ListEmployeeCompanyHolderName().ToArray();
        }

        public IEnumerable<string> GetToolsName()
        {
            return _checkinToolService.ListToolName();
        }
        
    }
}