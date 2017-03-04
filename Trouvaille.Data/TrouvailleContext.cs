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

        public IDbSet<Place> Places { get; set; }

        public static TrouvailleContext Create()
        {
            return new TrouvailleContext();
        }

        public System.Data.Entity.DbSet<Trouvaille.Models.Country> Countries { get; set; }
    }
}
