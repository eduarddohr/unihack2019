using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Unihack.Web.Startup))]
namespace Unihack.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
