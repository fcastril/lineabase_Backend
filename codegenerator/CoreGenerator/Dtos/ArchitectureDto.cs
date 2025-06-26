using System.Text.Json.Serialization;

namespace CoreGenerator.Dtos;

public class ArchitectureDto :  BaseSqliteDto
{
    public string Title { get; set; }        
    public string Description { get; set; }
    public string[] Layers { get; set; }  
}
