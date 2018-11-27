using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inVent.Web.Startup))]
namespace inVent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
