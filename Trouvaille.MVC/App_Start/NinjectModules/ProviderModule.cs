using Ninject.Modules;
using Trouvaille.Server.Common;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.MVC.App_Start.NinjectModules
{
    public class ProviderModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICacheProvider>().To<CacheProvider>();
            this.Bind<IUserProvider>().To<UserProvider>();
        }
    }
}