using System.Web;
using Trouvaille.Models;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Models
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
    }
}
