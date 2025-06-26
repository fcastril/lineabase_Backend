
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class SectorRepository : RepositoryBase<Sector>, ISectorRepository
    {

        public SectorRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}