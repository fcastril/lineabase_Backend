using Domain.Port;
using Infrastructure.Repository;
using Infrastructure.SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region CosmosDB for mongo
            services.Configure<ConfigurateCosmosDB>(option =>
            {
                option.ConnectionString = configuration[$"{nameof(ConfigurateCosmosDB)}:{nameof(option.ConnectionString)}"];
                option.DatabaseName = configuration[$"{nameof(ConfigurateCosmosDB)}:{nameof(option.DatabaseName)}"];
            });

            services.AddSingleton<IConfigurateCosmosDB>(sp => sp.GetRequiredService<IOptions<ConfigurateCosmosDB>>().Value);

            services.AddScoped<IMainContextCosmos, MainContextCosmosDB>();
            services.AddScoped<IMainContextSQLServer, MainContextSQLServer>();
            #endregion

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IRolRepository), typeof(RolRepository));

            return services;
        }
    }
}
