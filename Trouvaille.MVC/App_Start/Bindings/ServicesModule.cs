using Ninject.Modules;
using Trouvaille.Services;
using Trouvaille.Services.Common;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.App_Start.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {        
            this.Bind<IPlaceService>().To<PlaceService>();
            this.Bind<ICountryService>().To<CountryService>();
            this.Bind<IArticleService>().To<ArticleService>();
            this.Bind<IPictureService>().To<PictureService>();
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IMappingService>().To<MappingService>();
        }
    }
}