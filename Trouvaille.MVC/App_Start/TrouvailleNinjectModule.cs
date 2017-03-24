using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Common;
using Trouvaille.Services;
using Trouvaille.Services.Common;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC
{
    public class TrouvailleNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrouvailleContext>().To<TrouvailleContext>().InRequestScope();
            this.Bind(typeof(IEfGenericRepository<>)).To(typeof(EfGenericRepository<>));
            this.Bind<IUnitOfWork>().To<UnitOfWork>();

            this.Bind<IPlaceService>().To<PlaceService>();
            this.Bind<ICountryService>().To<CountryService>();
            this.Bind<IArticleService>().To<ArticleService>();
            this.Bind<IPictureService>().To<PictureService>();
            this.Bind<IUserService>().To<UserService>();

            this.Bind<IMappingService>().To<MappingService>();

            this.Bind<ICacheProvider>().To<CacheProvider>();
            this.Bind<IUserProvider>().To<UserProvider>();

            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InRequestScope();
        }
    }
}