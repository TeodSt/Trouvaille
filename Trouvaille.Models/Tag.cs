using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trouvaille.Models
{
    public class Tag
    {
        private ICollection<Picture> pictures; 
        private ICollection<Article> articles;

        public Tag()
        {
            this.pictures = new HashSet<Picture>();
            this.articles = new HashSet<Article>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
    }
}
