using System.Web.Mvc;

namespace WebSiteMjr.Areas.CustomerService
{
    public class CustomerServiceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CustomerService";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CustomerService_default",
                "CustomerService/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
