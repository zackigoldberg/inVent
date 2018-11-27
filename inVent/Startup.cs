using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inVent.Startup))]
namespace inVent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
