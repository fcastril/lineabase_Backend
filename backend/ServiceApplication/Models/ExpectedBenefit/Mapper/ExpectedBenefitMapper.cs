using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class ExpectedBenefitMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            CategoryMapper.Expresion(cnf);

            cnf.CreateMap<ExpectedBenefitDto, ExpectedBenefit>()
                .ConstructUsing(src => src != null ? new ExpectedBenefit(
                    src.Category,
                    src.Description,
                    src.ExpectedROI,
                    src.FigureFrom,
                    src.FigureTo,
                    src.DiscoveryId) : null);
        }
    }
}
