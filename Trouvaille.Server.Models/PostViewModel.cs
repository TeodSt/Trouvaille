using System.Collections.Generic;
using Trouvaille.Models;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Articles;

namespace Trouvaille.Server.Models
{
    public class PostViewModel : IMapFrom<Article>, IMapFrom<Picture>
    {
        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public IEnumerable<ArticleByIdViewModel> Articles { get; set; }     
    }
}
