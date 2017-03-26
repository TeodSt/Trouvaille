using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Models.Pictures
{
    public class AddPictureViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        public string Id { get; set; }
        
        public string CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public string ImagePath { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length must be 5 symbols")]
        [MaxLength(40, ErrorMessage = "Maximum length must be 40 symbols")]
        public string Description { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<CountryViewModel> Countries { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Picture, AddPictureViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom(src => src.CreatorId.ToString()))
                .ForMember(dest => dest.CreatorUsername, opts => opts.MapFrom(src => src.Creator.UserName));
        }
    }
}
