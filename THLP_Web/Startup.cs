using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(THLP_Web.Startup))]
namespace THLP_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
