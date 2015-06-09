using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Controllers.Api
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private readonly IMembershipService _membershipService;

        public UserController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        //[HttpGet]
        [AcceptVerbs("GET", "HEAD")]
        public UserViewModel GetUser(int userId)
        {
            var user = _membershipService.FindActiveUserById(userId);

            var userViewModel = new UserViewModel
            {
                CreateNewPassword = user.WantToResetPassword,
                UserId = user.Id,
                CompanyName = user.Employee.Company.Name,
                Username = user.Username
            };

            return userViewModel;
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage ChangeUserPassword(UserViewModel userViewModel)
        {
            try
            {
                _membershipService.CreateNewPassword(userViewModel.Username, userViewModel.NewPassword, userViewModel.UserSenaId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
        }
    }

    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserSenaId { get; set; }
        public bool CreateNewPassword { get; set; }
        public string NewPassword { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
    }
}