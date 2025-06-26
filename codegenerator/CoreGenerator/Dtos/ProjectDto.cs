using System.Text.Json.Serialization;

namespace CoreGenerator.Dtos;

public class ProjectDto :  BaseSqliteDto
{
    public ProjectDto():base()
    {
        
    }
    [JsonPropertyName("connectionString")]
    public string ConnectionString { get; set; } 

    [JsonPropertyName("projectName")]
    public string ProjectName { get; set; }    
}
