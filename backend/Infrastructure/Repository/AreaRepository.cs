
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {

        public AreaRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}