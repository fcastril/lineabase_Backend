using SqliteConnector.Common;
using SqliteConnector.Entities;

namespace SqliteConnector;

public class ProjectRepository : BaseRepository<ProjectEnt>,IProjectRepository
{
    public ProjectRepository(ISqliteContext sqliteContext) : base(sqliteContext)
    {
        
    }
}
