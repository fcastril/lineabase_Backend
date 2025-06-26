using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class BenefitMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            CategoryMapper.Expresion(cnf);

            cnf.CreateMap<BenefitDto, Benefit>()
				.ConstructUsing(src => src != null ? new Benefit(
                    src.Category,
                    src.CurrentProblem, 
                    src.CurrentTool,
                    src.NewTool, 
                    src.MigrationBenefit, 
                    src.IsRelevant,
                    src.DiscoveryId) : null);
        }
    }
}