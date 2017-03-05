using System.Data.Entity;
using Trouvaille.Data;

namespace Trouvaille.MVC
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