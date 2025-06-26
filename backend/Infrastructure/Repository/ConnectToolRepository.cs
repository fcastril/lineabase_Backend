using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class ConnectToolRepository : RepositoryBase<ConnectTool>, IConnectToolRepository
    {
        public ConnectToolRepository(IMainContextCosmos mainContext) : base(mainContext)
        {
        }
    }
}
