using Microsoft.EntityFrameworkCore;

namespace SqliteConnector;

public interface ISqliteContext
{
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
