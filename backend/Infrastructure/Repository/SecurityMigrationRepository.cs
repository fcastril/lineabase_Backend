using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class SecurityMigrationRepository : RepositoryBase<SecurityMigration>, ISecurityMigrationRepository
    {
        public SecurityMigrationRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
