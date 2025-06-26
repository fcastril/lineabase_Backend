namespace SqliteConnector.Entities;

public class ArchitectureEnt : BaseEntitySqlite
{
    public ArchitectureEnt() : base()
    {
        
    }
    public string Title { get; set; }        
    public string Description { get; set; }
    public string[] Layers { get; set; }
}
