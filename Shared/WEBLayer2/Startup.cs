using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEBLayer2.Startup))]
namespace WEBLayer2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
