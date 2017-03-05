using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Trouvaille.Models;

namespace Trouvaille.Data.Contracts
{
    public interface ITrouvailleContext
    {
        IDbSet<Article> Articles { get; set; }
        
        IDbSet<Continent> Continents { get; set; }  

        IDbSet<Country> Countries { get; set; }  

        IDbSet<Picture> Pictures { get; set; }

        IDbSet<Place> Places { get; set; }
        
        IDbSet<Tag> Tags { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
