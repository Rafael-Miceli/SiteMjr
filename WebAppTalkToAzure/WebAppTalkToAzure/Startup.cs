using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppTalkToAzure.Startup))]
namespace WebAppTalkToAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
