using System.Collections.Generic;
using Trouvaille.Models;
using Trouvaille.Server.Common;
using Trouvaille.Server.Models.Articles;

namespace Trouvaille.Server.Models
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }        
    }
}
