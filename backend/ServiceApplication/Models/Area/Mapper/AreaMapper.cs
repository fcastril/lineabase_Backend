
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class AreaMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<AreaDto, Area>()
				.ConstructUsing(src => src != null ? new Area(src.Description, src.Status): null);
			cnf.CreateMap<Area, AreaDto>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}