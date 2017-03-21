using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAllCountries();
        
        Country GetCountryById(int id);
    }
}
