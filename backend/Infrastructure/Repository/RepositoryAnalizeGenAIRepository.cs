using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class RepositoryAnalizeGenAIRepository : RepositoryBase<RepositoryAnalizeGenAI>, IRepositoryAnalizeGenAIRepository
    {
        public RepositoryAnalizeGenAIRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
