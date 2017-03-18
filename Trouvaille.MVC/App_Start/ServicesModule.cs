using Ninject.Modules;
using Ninject.Web.Common;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;
using Trouvaille.Services;
using Trouvaille.Services.Common;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrouvailleContext>().To<TrouvailleContext>().InSingletonScope();
            this.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            this.Bind<IUnitOfWork>().To<UnitOfWork>();

            this.Bind<IPlaceService>().To<PlaceService>();
            this.Bind<ICountryService>().To<CountryService>();
            this.Bind<IArticleService>().To<ArticleService>();
            this.Bind<IPictureService>().To<PictureService>();

            this.Bind<IMappingService>().To<MappingService>();
        }
    }
}