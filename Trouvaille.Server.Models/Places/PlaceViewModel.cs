using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models.Places
{
    public class PlaceViewModel : IMapFrom<Place>, IHaveCustomMappings
    {
        public int Id { get; set; }
        
        public string FounderId { get; set; }

        public string FounderName { get; set; }

        public double Longtitude { get; set; }
        
        public double Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Place, PlaceViewModel>()
                .ForMember(dest => dest.FounderId, opts => opts.MapFrom(src => src.FounderId.ToString()))
                .ForMember(dest => dest.FounderName, opts => opts.MapFrom(src => src.Founder.UserName));
        }
    }
}
