using Microsoft.Owin;
using Owin;
using ibby_cms.App_Start;

[assembly: OwinStartupAttribute(typeof(ibby_cms.Startup))]
namespace ibby_cms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            InitializationRoles.CreateRolesanUsers(); // отдельный статический класс и вызвать
        }
    }
}
