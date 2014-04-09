using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToTheMoon.Startup))]
namespace ToTheMoon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
