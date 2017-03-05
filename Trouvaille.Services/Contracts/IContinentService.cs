using System;
using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IContinentService
    {
        IEnumerable<Continent> GetAllContinents();

        Continent GetContinentById(int id);
    }
}
