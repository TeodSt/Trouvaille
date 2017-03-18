using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public string Subheader { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public PrivacyType PrivacyType { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
