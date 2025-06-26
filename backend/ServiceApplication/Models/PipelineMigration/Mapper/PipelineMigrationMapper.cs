using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class PipelineMigrationMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<PipelineMigrationDto, PipelineMigration>()
                .ConstructUsing(src => src != null ? new PipelineMigration(
                    src.DiscoveryId,
                    src.Name,
                    src.StatusLastExecution,
                    src.SourceBranch,
                    src.LastExecution) : null);
        }
    }
}
