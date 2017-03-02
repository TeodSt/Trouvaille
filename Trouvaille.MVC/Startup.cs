using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trouvaille.MVC.Startup))]
namespace Trouvaille.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
