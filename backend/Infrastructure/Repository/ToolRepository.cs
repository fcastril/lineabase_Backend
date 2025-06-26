
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ToolRepository : RepositoryBase<Tool>,IToolRepository
    {

        public ToolRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}