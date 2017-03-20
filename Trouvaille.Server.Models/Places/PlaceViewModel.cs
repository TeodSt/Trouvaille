﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Models;
using Trouvaille.Server.Common;

namespace Trouvaille.Server.Models.Places
{
    public class PlaceViewModel : IMapFrom<Place>, IHaveCustomMappings
    {
        public int Id { get; set; }
        
        public string FounderId { get; set; }
        
        public double Longtitude { get; set; }
        
        public double Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }
        
        public void CreateMappings(IMapperConfigurationExpression congif)
        {
            congif.CreateMap<Place, AddPlaceViewModel>()
                .ForMember(dest => dest.FounderId, opts => opts.MapFrom(src => src.FounderId.ToString()))
                .ForMember(dest => dest.Countries, opts => opts.Ignore());
        }
    }
}
