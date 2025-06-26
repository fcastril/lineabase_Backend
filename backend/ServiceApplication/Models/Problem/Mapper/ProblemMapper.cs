
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class ProblemMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<ProblemDto, Problem>()
				.ConstructUsing(src => src != null ? new Problem(src.Description, src.Status): null);
			cnf.CreateMap<Problem, ProblemDto>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}