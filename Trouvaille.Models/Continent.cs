using System.ComponentModel.DataAnnotations;

namespace Trouvaille.Models
{
    public class Continent
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
