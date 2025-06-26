using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class RepositoryAnalizeGenAIMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<RepositoryAnalizeGenAIDto, RepositoryAnalizeGenAI>()
				.ConstructUsing(src => src != null ? new RepositoryAnalizeGenAI(
                    src.DiscoveryId,
                    src.RepositoryId,
                    src.RepositoryName,
                    src.ValidationRule,
                    src.Status,
                    src.ProblemDetail,
                    src.Suggestion) : null);
        }
    }
}