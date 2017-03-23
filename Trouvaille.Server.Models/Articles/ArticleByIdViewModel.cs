using System;
using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models.Articles
{
    public class ArticleByIdViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public string Subheader { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CountryName { get; set; }
    }
}
