using Trouvaille.Data;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class PlaceService : IPlaceService
    {
        // TODO: DI
        private static TrouvailleContext context = new TrouvailleContext();

        public void AddPlace(Place place)
        {
            context.Places.Add(place);

            context.SaveChanges();
        }
    }
}
