using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trouvaille.Models.Enums;

namespace Trouvaille.Models
{
    public class Picture
    {
        private ICollection<Tag> tags;

        public Picture()
        {
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public string Path { get; set; }

        public PrivacyType PrivacyType { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
