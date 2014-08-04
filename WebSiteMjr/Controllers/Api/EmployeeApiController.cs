﻿using System.Collections.Generic;
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
        public User GetEmployeeUser(string employeeId)
        {
            return null;
            //return _membershipService.FindUserByEmployeeId(_employeeService.FindEmployee(employeeId).Id);
        }
    }
}