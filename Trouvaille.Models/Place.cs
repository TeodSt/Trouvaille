namespace Trouvaille.Models
{
    public class Place
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }
    }
}
