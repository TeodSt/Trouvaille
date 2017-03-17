using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
