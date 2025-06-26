
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class FrecuencyRepository : RepositoryBase<Frecuency>,IFrecuencyRepository
    {

        public FrecuencyRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}