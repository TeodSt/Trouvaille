using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trouvaille.MVC.App_Start.NinjectModules
{
    public class TrouvailleModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InRequestScope();
        }
    }
}