using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ExpectedBenefitRepository : RepositoryBase<ExpectedBenefit>, IExpectedBenefitRepository
    {
        public ExpectedBenefitRepository(IMainContextCosmos mainContext) : base(mainContext) { }
    }
}
