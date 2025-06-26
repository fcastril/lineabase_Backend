using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class EstimationOverviewMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            CategoryMapper.Expresion(cnf);

            cnf.CreateMap<EstimationOverviewDto, EstimationOverview>()
                .ConstructUsing(src => src != null ? new EstimationOverview(
                    src.DiscoveryId,
                    src.Activity,
                    src.Description,
                    src.Time,
                    src.Dependency,
                    src.Category,
                    src.AutomationMigration) : null );
        }
    }
}
