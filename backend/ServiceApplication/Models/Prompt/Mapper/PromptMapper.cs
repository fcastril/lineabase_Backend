using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class PromptMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<PromptDto, Prompt>()
				.ConstructUsing(src => src != null ? new Prompt(
                    src.Name,
                    src.Body, 
                    src.Overrides) : null);
        }
    }
}