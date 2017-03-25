using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IPlaceService
    {
        IEnumerable<Place> GetAllPlaces();

        void AddPlace(Place place);        
    }
}
