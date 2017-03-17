using AutoMapper;

namespace Trouvaille.Server.Common
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression congif);
    }
}