using System.Collections.Generic;

namespace Trouvaille.Server.Models.Articles
{
    public class ArticlesViewModel
    {
        public IEnumerable<AddArticleViewModel> Articles { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}
