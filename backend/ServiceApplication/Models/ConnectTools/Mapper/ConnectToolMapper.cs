using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public class ConnectToolMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<ConnectToolDto, ConnectTool>()
                .ConstructUsing(src => src != null ? new ConnectTool(
                    src.DiscoveryId,
                    src.Organization,
                    src.PAT) : null);
        }
    }
}
