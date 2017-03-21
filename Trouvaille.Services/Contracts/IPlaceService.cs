using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IPlaceService
    {
        IEnumerable<Place> GetAllPlaces();

        IEnumerable<Place> GetPlacesByContinent(string continentName);

        Place GetPlaceById(int id); 

        void AddPlace(Place place);

        void DeletePlace(Place place);
    }
}
