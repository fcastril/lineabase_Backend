using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class CurrentToolRepository : RepositoryBase<CurrentTool>, ICurrentToolRepository
    {

        public CurrentToolRepository(IMainContextCosmos mainContext) : base(mainContext)
        {


        }

    }
}