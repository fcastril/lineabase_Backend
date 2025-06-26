using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class PackageMigrationRepository : RepositoryBase<PackageMigration>, IPackageMigrationRepository
    {
        public PackageMigrationRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
