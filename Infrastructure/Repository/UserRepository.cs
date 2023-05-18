
using Domain.Entities;
using Domain.Port;
using Infrastructure.Repository.Bases;

namespace Infrastructure.Repository
{
    public class UserRepository : RepositoryBaseSQLServer<User>, IRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
        {

        }
    }
}
