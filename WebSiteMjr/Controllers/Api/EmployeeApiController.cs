using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Controllers.Api
{
    public class EmployeeApiController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMembershipService _membershipService;

        public EmployeeApiController(IEmployeeService employeeService, IMembershipService membershipService)
        {
            _employeeService = employeeService;
            _membershipService = membershipService;
        }

        //[HttpGet]
        [AcceptVerbs("GET", "HEAD")]
        public bool GetEmployeeUser(string employeeId)
        {
            return _membershipService.FindActiveUserByEmployeeId(int.Parse(employeeId)) != null;
        }
    }
}