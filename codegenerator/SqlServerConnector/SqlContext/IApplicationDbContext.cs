namespace SqlServerConnector.SqlContext;

public interface IApplicationDbContext
{
    Task< List<DbTables>> TablesAvailable(string connectionString);
}
