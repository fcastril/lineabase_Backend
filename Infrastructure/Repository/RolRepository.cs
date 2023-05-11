using Domain.Entities;
using Domain.Port;
using Infrastructure.Repository.Bases;

namespace Infrastructure.Repository
{
    public class RolRepository : RepositoryBaseSQLServer<Rol>, IRepositoryBase<Rol>, IRolRepository
    {
        public RolRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
        {

        }
    }
}
