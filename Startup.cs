using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(adminNaturguiden.Startup))]
namespace adminNaturguiden
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
