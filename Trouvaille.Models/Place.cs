using System;
using System.ComponentModel.DataAnnotations;

namespace Trouvaille.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required]
        public string FounderId { get; set; }

        public virtual User Founder { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Address { get; set; }

        [Required]
        public double Longtitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}
