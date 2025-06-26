
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class DiscoveryRepository : RepositoryBase<Discovery>,  IDiscoveryRepository
    {

        public DiscoveryRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}