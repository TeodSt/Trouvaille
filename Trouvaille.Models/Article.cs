using System.Collections.Generic;

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

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
