
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ProblemDetailsRepository : RepositoryBase<ProblemDetails>, IRepositoryBase<ProblemDetails>, IProblemDetailsRepository
    {

        public ProblemDetailsRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}