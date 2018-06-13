using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ibby_cms.Startup))]
namespace ibby_cms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
