using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class RepositoryMigrationRepository : RepositoryBase<RepositoryMigration>, IRepositoryMigrationRepository
    {
        public RepositoryMigrationRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
