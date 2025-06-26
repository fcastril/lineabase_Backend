using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class BenefitService : BaseServiceApplication<Benefit, BenefitDto>, IBenefitService
    {
        public BenefitService(IBenefitRepository benefitRepository, ICategoryService categoryService): base(benefitRepository)
        {
            CreateMapperExpresion<Benefit, BenefitDto>(cnf =>
            {
                BenefitMapper.Expresion(cnf);
            });
        }
    }
}