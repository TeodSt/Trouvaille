using Ninject.Modules;
using Ninject.Web.Common;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;
using Trouvaille.Services;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrouvailleContext>().To<TrouvailleContext>().InRequestScope();
            this.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            this.Bind<IUnitOfWork>().To<UnitOfWork>();

            this.Bind<IPlaceService>().To<PlaceService>();
        }
    }
}