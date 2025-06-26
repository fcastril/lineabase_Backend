using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class PromptRepository : RepositoryBase<Prompt>, IPromptRepository
    {
        public PromptRepository(IMainContextCosmos mainContext) : base(mainContext) { }
    }
}
