﻿using System;
using AutoMapper;
using Trouvaille.Services.Common.Contracts;

namespace Trouvaille.Services.Common
{
    public class MappingService : IMappingService
    {
        public T Map<T>(object source)
        {
           return Mapper.Map<T>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
