using System.Collections.Generic;
using Trouvaille.Models;
using Trouvaille.Server.Common;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;

namespace Trouvaille.Server.Models.Users
{
    public class UserProfileViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<ArticleByIdViewModel> Articles { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }
    }
}
