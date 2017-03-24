using System;
using Ninject.Modules;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;
using Ninject.Web.Common;

namespace Trouvaille.MVC.App_Start.NinjectModules
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrouvailleContext>().To<TrouvailleContext>().InRequestScope();
            this.Bind(typeof(IEfGenericRepository<>)).To(typeof(EfGenericRepository<>));
            this.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}