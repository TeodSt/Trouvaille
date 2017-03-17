using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models.Places
{
    public class AddPlaceViewModel : IMapFrom<Place>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string FounderId { get; set; }

        [Required(ErrorMessage = "Longtitude is required.")]
        public double Longtitude { get; set; }

        [Required(ErrorMessage = "Latitude is required.")]
        public double Latitude { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimun length must be 3 symbols")]
        [MaxLength(20, ErrorMessage = "Maximum length must be 20 symbols")]
        public string Address { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimun length must be 3 symbols")]
        public string Description { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<CountryViewModel> Countries { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Place, AddPlaceViewModel>()
                .ForMember(dest => dest.FounderId, opts => opts.MapFrom(src => src.FounderId.ToString()))
                .ForMember(dest => dest.Countries, opts => opts.Ignore());            
        }
    }
}
