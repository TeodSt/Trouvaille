using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IPlaceService
    {
        void AddPlace(Place place);
    }
}
