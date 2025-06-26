using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class RolRepository : RepositoryBase<Rol>, IRolRepository
    {
        public RolRepository(IMainContextCosmos mainContext)
            : base(mainContext)
        {

        }
    }
}
