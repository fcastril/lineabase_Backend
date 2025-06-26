
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class SectorMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<SectorDto, Sector>()
				.ConstructUsing(src => src != null ? new Sector(src.Description, src.Status): null);
			cnf.CreateMap<Sector, SectorDto>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}