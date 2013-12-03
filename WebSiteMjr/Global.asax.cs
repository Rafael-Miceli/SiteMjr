
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSiteMjr.EfConfigurationMigrationData;
using WebSiteMjr.EfConfigurationMigrationData.Migrations;

namespace WebSiteMjr
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MjrSolutionContext, MjrSolutionConfiguration>());
            Database.SetInitializer(new MjrSolutionConfiguration());
            var context = new MjrSolutionContext();
            context.Database.Initialize(true);
            //context.Database.CreateIfNotExists();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //RegisterUnityConfig.RegisterContainers();
            Bootstrapper.Initialise();
        }
    }
}