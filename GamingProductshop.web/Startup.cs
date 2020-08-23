using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamingProductshop.web.Startup))]
namespace GamingProductshop.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
