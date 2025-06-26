
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ProblemRepository : RepositoryBase<Problem>, IProblemRepository
    {

        public ProblemRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}