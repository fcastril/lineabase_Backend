
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class FrecuencyMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<FrecuencyDto, Frecuency>()
				.ConstructUsing(src => src != null ? new Frecuency(src.Name, src.Status): null);
			cnf.CreateMap<Frecuency, FrecuencyDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}