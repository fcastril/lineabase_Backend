using System.Text.Json.Serialization;

namespace SqliteConnector.Entities;

public class ProjectEnt : BaseEntitySqlite
{ 

    public ProjectEnt():base()
    {

    }
    [JsonPropertyName("connectionString")]
    public string ConnectionString { get; set; } 
    [JsonPropertyName("projectName")]
    public string ProjectName { get; set; }      
}
