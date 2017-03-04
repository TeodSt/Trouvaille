using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trouvaille.Models;

namespace Trouvaille.Data
{
    public class TrouvailleContext : IdentityDbContext<User>
    {
        public TrouvailleContext() : base("Trouvaille")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TrouvailleContext>());
        }

        public static TrouvailleContext Create()
        {
            return new TrouvailleContext();
        }
    }
}
