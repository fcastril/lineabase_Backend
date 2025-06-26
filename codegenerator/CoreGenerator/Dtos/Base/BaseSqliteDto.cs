using System.Text.Json.Serialization;

namespace CoreGenerator.Dtos;

public class BaseSqliteDto
{
    public BaseSqliteDto()
    {
    }
    public int Id { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
}
