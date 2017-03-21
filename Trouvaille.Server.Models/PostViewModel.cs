using System.Collections.Generic;
using Trouvaille.Models;
using Trouvaille.Server.Common;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Articles;

namespace Trouvaille.Server.Models
{
    public class PostViewModel : IMapFrom<Article>, IMapFrom<Picture>
    {
        public IEnumerable<AddPictureViewModel> Pictures { get; set; }

        public IEnumerable<AddArticleViewModel> Articles { get; set; }     
    }
}
