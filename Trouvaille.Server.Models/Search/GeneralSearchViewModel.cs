using System.Collections.Generic;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;

namespace Trouvaille.Server.Models.Search
{
    public class GeneralSearchViewModel
    {
        public string Text { get; set; }

        public IEnumerable<ArticleByIdViewModel> Articles { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
