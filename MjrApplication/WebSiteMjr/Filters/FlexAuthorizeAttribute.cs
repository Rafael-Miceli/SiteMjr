using System;
using System.Linq;
using System.Web.Mvc;
using FlexProviders.Roles;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.Filters
{
    public class FlexAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public FlexRoleProvider RoleProvider;

        public string Roles
        {
            get
            {
                return _roles ?? string.Empty;
            }
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }

        public string Users
        {
            get
            {
                return _users ?? string.Empty;
            }
            set
            {
                _users = value;
                _usersSplit = SplitString(value);
            }
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        protected string[] SplitString(string original)
        {
            return original.Split(',').Select(s => s.Trim()).ToArray();
        }

        private string[] _rolesSplit = new string[0];
        private string[] _usersSplit = new string[0];
        private string _roles;
        private string _users;

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (_usersSplit.Length > 0)
            {
                if (_usersSplit.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            if (_rolesSplit.Length > 0)
            {
                RoleProvider = new FlexRoleProvider(new RoleRepository<MjrAppRole, User>());
                if (_rolesSplit.Any(role => RoleProvider.IsUserInRole(user.Identity.Name, role)))
                {
                    return;
                }
            }

            if (_rolesSplit.Length > 0 || _usersSplit.Length > 0)
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}