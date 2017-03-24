using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trouvaille.Models.Enums;

namespace Trouvaille.Models
{
    public class Picture
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string  Description { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
