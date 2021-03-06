﻿using System.Reflection;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Trouvaille.MVC.App_Start;

namespace Trouvaille.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string ServerModelsAssembly = "Trouvaille.Server.Models";
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;
         Initial Catalog=Trouvaille;Integrated Security=True";
        private const string CountriesTable = "Countries";

        protected void Application_Start()
        {
            DbConfig.InitializeDatabase();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Config(Assembly.Load(ServerModelsAssembly));

            SqlCacheDependencyAdmin.EnableNotifications(ConnectionString);
            SqlCacheDependencyAdmin.EnableTableForNotifications(ConnectionString, CountriesTable);
        }
    }
}
