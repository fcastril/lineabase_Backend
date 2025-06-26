using Microsoft.EntityFrameworkCore;
using SqliteConnector.Entities;

namespace SqliteConnector;

public class SqliteContext: DbContext,ISqliteContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string currentDirectory = AppContext.BaseDirectory;
        string? projectRoot = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        string realPath = $"{projectRoot}/SqliteConnector";
        optionsBuilder.UseSqlite(@"Data Source="+realPath+"/GeneratorContext.db;");
    }

    public virtual DbSet<ArchitectureEnt> ArchitectureEnts { get; set; }
    public virtual DbSet<ProjectEnt> ProjectEnts { get; set; }

}


