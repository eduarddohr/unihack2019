using Microsoft.Owin;
using Owin;
using System.Net.Http;

[assembly: OwinStartupAttribute(typeof(Unihack.Web.Startup))]
namespace Unihack.Web
{
    public partial class Startup
    {
        public static HttpClient client;
        public void Configuration(IAppBuilder app)
        {
            client = new HttpClient();
            ConfigureAuth(app);
        }
    }
}
