using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trouvaille.Models.Enums;

namespace Trouvaille.Models
{
    public class Article
    {
        private ICollection<Tag> tags;

        public Article()
        {
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public string Subheader { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public PrivacyType PrivacyType { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
