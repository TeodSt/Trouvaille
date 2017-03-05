using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface ICountryService
    {
        ICollection<Country> GetAllCountries();

        Country GetCountryById(int id);
    }
}
