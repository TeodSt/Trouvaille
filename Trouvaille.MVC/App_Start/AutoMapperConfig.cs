using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.MVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config(params Assembly[] assemblies)
        {
            Mapper.Initialize(c => RegisterMappings(c, assemblies));
        }

        public static void RegisterMappings(IMapperConfigurationExpression congif, params Assembly[] assemblies)
        {
            congif.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }

            LoadStandardMappings(congif, types);
            LoadCustomMappings(congif, types);

            //ExplicitMaps.AddMaps(Mapper.Configuration);
        }

        private static void LoadStandardMappings(IMapperConfigurationExpression congif, IEnumerable<Type> types)
        {
            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.t.IsAbstract
                        && !type.t.IsInterface)
                .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

            foreach (var map in maps)
            {
                congif.CreateMap(map.Source, map.Destination);
                congif.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IMapperConfigurationExpression congif, IEnumerable<Type> types)
        {
            var maps =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(IHaveCustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(congif);
            }
        }
    }
}