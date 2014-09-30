using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Facade;

namespace WebSiteMjr
{
    public class WebUtils
    {
        private static IEmployeeLoginFacade _employeeLoginFacade;

        private WebUtils()
        {}

        public static void Create(IEmployeeLoginFacade employeeLoginFacade)
        {
            _employeeLoginFacade = employeeLoginFacade;
        }

        private static User GetLoggedUser(Controller controller)
        {
            return _employeeLoginFacade.GetLoggedUser(controller.User.Identity.Name);
        }
    }
}