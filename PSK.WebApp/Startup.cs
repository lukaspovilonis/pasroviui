using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PSK.WebApp.Startup))]
namespace PSK.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
