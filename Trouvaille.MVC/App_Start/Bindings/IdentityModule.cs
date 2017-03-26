using Microsoft.AspNet.Identity.Owin;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;
using Trouvaille.Server.Identity;
using Trouvaille.Server.Identity.Contracts;

namespace Trouvaille.MVC.App_Start.Bindings
{
    public class IdentityModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IApplicationSignInManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>()).InRequestScope();
            this.Bind<IApplicationUserManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).InRequestScope();
        }
    }
}