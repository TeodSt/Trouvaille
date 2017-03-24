using System.Collections.Generic;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Users;

namespace Trouvaille.Server.Models.Search
{
    public class GeneralSearchViewModel
    {
        public string Text { get; set; }

        public IEnumerable<AddArticleViewModel> Articles { get; set; }

        public IEnumerable<AddPictureViewModel> Pictures { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
