
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class CurrentToolMapper
    {

        public static void Expresion(IMapperConfigurationExpression cnf,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService,
            IBaseServiceApplication<Discovery, DiscoveryDto> discoveryService,
            IBaseServiceApplication<Tool, ToolDto> toolService,
            IBaseServiceApplication<Area, AreaDto> areaService
)
        {
            DiscoveryMapper.Expresion(cnf, customerService, sectorService);
            ToolMapper.Expresion(cnf);
            AreaMapper.Expresion(cnf);

            cnf.CreateMap<CurrentToolDto, CurrentTool>()
                .ConstructUsing(src => src != null ? new CurrentTool(
                    discoveryService.MapToENT<Discovery, DiscoveryDto>(src.Discovery),
                    toolService.MapToENT<Tool, ToolDto>(src.Tool),
                    src.NameServer) : null);
            cnf.CreateMap<CurrentTool, CurrentToolDto>()
                .ForMember(dest => dest.Discovery, opt => opt.MapFrom(src => src.Discovery))
                .ForMember(dest => dest.Tool, opt => opt.MapFrom(src => src.Tool))
                .ForMember(dest => dest.NameServer, opt => opt.MapFrom(src => src.NameServer));
        }
    }
}