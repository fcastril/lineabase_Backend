using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class BenefitRepository : RepositoryBase<Benefit>, IBenefitRepository
    {
        public BenefitRepository(IMainContextCosmos mainContext) : base(mainContext) { }
    }
}
