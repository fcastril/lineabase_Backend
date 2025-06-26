using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class UserMigrationRepository : RepositoryBase<UserMigration>, IUserMigrationRepository
    {
        public UserMigrationRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
