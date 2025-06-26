using SqliteConnector.Common;
using SqliteConnector.Entities;

namespace SqliteConnector;

public class ArchitectureRepository : BaseRepository<ArchitectureEnt>,IArchitectureRepository
{
    public ArchitectureRepository(ISqliteContext sqliteContext) : base(sqliteContext)
    {
        
    }
}
