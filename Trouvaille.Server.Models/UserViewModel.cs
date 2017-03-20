using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
