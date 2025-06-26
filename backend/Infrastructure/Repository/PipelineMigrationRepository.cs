using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class PipelineMigrationRepository : RepositoryBase<PipelineMigration>, IPipelineMigrationRepository
    {
        public PipelineMigrationRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
