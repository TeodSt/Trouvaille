using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;

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