using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Trouvaille.Data;
using Trouvaille.Data.Contracts;

namespace Trouvaille.MVC.App_Start
{
    public class DbConfig
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrouvailleContext, Trouvaille.Data.Migrations.Configuration>());
            TrouvailleContext.Create().Initialize();
        }
    }
}