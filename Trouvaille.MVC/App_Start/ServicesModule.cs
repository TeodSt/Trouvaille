using Ninject.Modules;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;
using Trouvaille.Services;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.App_Start
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrouvailleContext>().To<TrouvailleContext>();

            this.Bind<IPlaceService>().To<PlaceService>();
        }
    }
}