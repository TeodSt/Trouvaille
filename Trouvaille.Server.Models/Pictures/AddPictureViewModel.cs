﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models.Pictures
{
    public class AddPictureViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public string Path { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length must be 5 symbols")]
        [MaxLength(40, ErrorMessage = "Maximum length must be 40 symbols")]
        public string Description { get; set; }

        public string PrivacyType { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Picture, AddPictureViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom(src => src.CreatorId.ToString()))
                .ForMember(dest => dest.CreatorUsername, opts => opts.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.PrivacyType, opts => opts.MapFrom(src => src.PrivacyType.ToString()));
        }
    }
}