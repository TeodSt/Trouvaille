using System;
using Trouvaille.Models;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Models.Pictures
{
    public class PictureViewModel : IMapFrom<Picture>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CountryName { get; set; }
    }
}
