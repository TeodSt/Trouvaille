using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models
{
    public class PostViewModel : IMapFrom<Article>, IMapFrom<Picture>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CreatorUsername { get; set; }

        public string CreatorId { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Picture, PostViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom(src => src.CreatorId.ToString()))
                .ForMember(dest => dest.ImagePath, opts => opts.MapFrom(src => src.Path))
                .ForMember(dest => dest.CreatorUsername, opts => opts.MapFrom(src => src.Creator.UserName));

            congif.CreateMap<Article, PostViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom(src => src.CreatorId.ToString()))
                .ForMember(dest => dest.CreatorUsername, opts => opts.MapFrom(src => src.Creator.UserName));
        }
    }
}
