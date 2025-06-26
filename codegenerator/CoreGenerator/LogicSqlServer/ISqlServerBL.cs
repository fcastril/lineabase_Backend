using CoreGenerator.Entities;

namespace CoreGenerator.LogicSqlServer;

public interface ISqlServerBL
{
    Task<List<TableEnt>> GetTablesAvailable(string codeProject);
    Task<Dictionary<string, string>> GetTableProperties(string codeProject,string tableName);
}
