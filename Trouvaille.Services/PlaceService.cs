using Trouvaille.Data;
using Trouvaille.Models;

namespace Trouvaille.Services
{
    public class PlaceService
    {
        // TODO: DI
        private static TrouvailleContext context = new TrouvailleContext();

        public void AddPlace(double longtitude, double latitude)
        {
            context.Places.Add(new Place()
            {
                FounderId = 1,
                Address = "need to find by coords",
                Description = "pretty place",
                Longtitude = longtitude,
                Latitude = latitude
            });

            context.SaveChanges();
        }
    }
}
