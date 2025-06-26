using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    internal class EstimationOverviewRepository : RepositoryBase<EstimationOverview>, IEstimationOverviewRepository
    {
        public EstimationOverviewRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
