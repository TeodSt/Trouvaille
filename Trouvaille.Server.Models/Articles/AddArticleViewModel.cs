﻿using System;
using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common;
using System.ComponentModel.DataAnnotations;

namespace Trouvaille.Server.Models.Articles
{
    public class AddArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length must be 5 symbols")]
        [MaxLength(40, ErrorMessage = "Maximum length must be 40 symbols")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length must be 5 symbols")]
        public string Content { get; set; }

        public string Subheader { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUsername { get; set; }

        public string PrivacyType { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Article, AddArticleViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom(src => src.CreatorId.ToString()))
                .ForMember(dest => dest.CreatorUsername, opts => opts.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.PrivacyType, opts => opts.MapFrom(src => src.PrivacyType.ToString()));
        }
    }
}