using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trouvaille.Models
{
    public class Article
    {
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

        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string ImagePath { get; set; }
    }
}
