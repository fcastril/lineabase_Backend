using CoreGenerator.Entities;
using CoreGenerator.LogicSqlServer;
using SqlServerConnector.SqlContext;

namespace CoreGenerator;

public class SqlServerBL:ISqlServerBL
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IProjectBL _projectBL;

    public SqlServerBL(IApplicationDbContext applicationDbContext, IProjectBL projectBL)
    {
        _applicationDbContext = applicationDbContext;
        _projectBL = projectBL;
    }

    public async Task<List<TableEnt>> GetTablesAvailable(string codeProject)
    {
        var project = await _projectBL.FirstOrDefault(f=>f.Code == codeProject);
        if (project == null)
            throw new Exception("Project selected not found");

        string connectionString = project.ConnectionString;
        var responseTables = await _applicationDbContext.TablesAvailable(connectionString);

        return responseTables.ToList().Select(s=>s.Name).Distinct().Select(
            s=>new TableEnt{
                Name = s,
                Schemma = responseTables.FirstOrDefault(f=>f.Name == s)?.Schemma??"WithOutSchemma",
            }
         ).ToList();
    }

    public async Task<Dictionary<string, string>> GetTableProperties(string codeProject,string tableName)
    {
        var restDiccionario = new Dictionary<string, string>();
        List<string> balckList = new List<string>(){"id","code","status"};
        var project = await _projectBL.FirstOrDefault(f=>f.Code == codeProject);
        if (project == null)
            throw new Exception("Project selected not found");

        string connectionString = project.ConnectionString;
        var responseTables = await _applicationDbContext.TablesAvailable(connectionString);

        var rest = responseTables.ToList().Where(w=>w.Name==tableName).Select(s=> new {property=s.Property,type=s.Type}).ToArray()
                .Where(s=>!balckList.Contains(s.property.ToLower())).ToArray().Select(ss=> ss).ToList();

        rest.ForEach(s=>restDiccionario.Add(s.property,s.type));
        return restDiccionario;
    }
}
