
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class ProblemDetailsMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<ProblemDetailsDto, ProblemDetails>()
				.ConstructUsing(src => src != null ? new ProblemDetails(src.Discovery, src.Category, src.Problem, src.Impact, src.Frecuency, src.Description): null);
			cnf.CreateMap<ProblemDetails, ProblemDetailsDto>()
				.ForMember(dest => dest.Discovery, opt => opt.MapFrom(src => src.Discovery))
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
				.ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem))
				.ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact))
				.ForMember(dest => dest.Frecuency, opt => opt.MapFrom(src => src.Frecuency))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}