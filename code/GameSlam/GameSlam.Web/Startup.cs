using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameSlam.Web.Startup))]
namespace GameSlam.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
