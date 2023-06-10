using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Constants;
using Domain.Port;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer : DbContext, IMainContextSQLServer
	{

        private readonly IConfiguration _configuration;

        public MainContextSQLServer()
        {
        }

        public MainContextSQLServer(DbContextOptions<MainContextSQLServer> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public MainContextSQLServer(DbContextOptions<MainContextSQLServer> options) : base(options)
		{
		}
		public MainContextSQLServer(string connectionString) : base(GetOptions(connectionString,null))
		{
		}
		public MainContextSQLServer(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
			: base(GetOptions(connectionString, sqlServerOptionsAction))
        {

		}
		private static DbContextOptions GetOptions(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
		{
			return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString, sqlServerOptionsAction).Options;
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.HasAnnotation("Relation:Collation", SQLServerConstants.RelationCollation);
			modelBuilder.HasDefaultSchema(SQLServerConstants.DefaultSchema);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContextSQLServer).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration[$"{nameof(ConfigurateSQLServer)}:{nameof(ConfigurateSQLServer.ConnectionString)}"];
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}

