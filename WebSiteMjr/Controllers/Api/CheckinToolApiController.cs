using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;
using WebSiteMjr.EfStuffData.DataRepository;
using WebSiteMjr.EfStuffData.UnitOfWork;

namespace WebSiteMjr.Controllers.Api
{
    public class CheckinToolApiController : ApiController
    {
        private readonly ICheckinToolService _checkinToolService;

        public CheckinToolApiController()
        {
            //TOdo Please correct this Ugly shit
            var personUow = new PersonsUow();
            var stuffUnow = new StuffUow();
            var companyServiceInstance = new CompanyService(new CompanyRepository(personUow), personUow);
            var employeeServiceInstance = new EmployeeService(new EmployeeRepository(personUow), personUow);
            var toolServiceInstance = new ToolService(new ToolRepository(stuffUnow), stuffUnow);

            _checkinToolService = new CheckinToolService(new CheckinToolRepository(stuffUnow), stuffUnow, companyServiceInstance, employeeServiceInstance, toolServiceInstance); 
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

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}