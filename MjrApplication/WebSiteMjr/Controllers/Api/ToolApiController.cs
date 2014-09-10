using System.Collections.Generic;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Controllers.Api
{
    public class ToolApiController : ApiController
    {
        private readonly IToolService _toolService;

        public ToolApiController(IToolService toolService)
        {
            _toolService = toolService;
        }

        public IEnumerable<string> GetToolsName()
        {
            return _toolService.ListToolName();
        }
        
    }
}