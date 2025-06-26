using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class ExpectedBenefitService : BaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto>, IExpectedBenefitService
    {
        public ExpectedBenefitService(IExpectedBenefitRepository expectedBenefitRepository) : base(expectedBenefitRepository)
        {
            CreateMapperExpresion<ExpectedBenefit, ExpectedBenefitDto>(cnf =>
            {
                ExpectedBenefitMapper.Expresion(cnf);
            });
        }
    }
}
