
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class CurrentToolMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<CurrentToolDto, CurrentTool>()
				.ConstructUsing(src => src != null ? new CurrentTool(src.Discovery, src.Tool, src.Area, src.NameServer): null);
			cnf.CreateMap<CurrentTool, CurrentToolDto>()
				.ForMember(dest => dest.Discovery, opt => opt.MapFrom(src => src.Discovery))
				.ForMember(dest => dest.Tool, opt => opt.MapFrom(src => src.Tool))
				.ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
				.ForMember(dest => dest.NameServer, opt => opt.MapFrom(src => src.NameServer));
        }
    }
}