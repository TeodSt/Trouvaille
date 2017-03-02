namespace Trouvaille.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
    }
}
