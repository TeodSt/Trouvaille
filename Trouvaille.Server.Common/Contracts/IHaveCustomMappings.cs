using AutoMapper;

namespace Trouvaille.Server.Common.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression congif);
    }
}