using Domain.Port;
using Infrastructure.Integrations;
using Infrastructure.Repository;
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
            #endregion

            services.AddScoped(typeof(ISecurityRepository), typeof(SecurityRepository));
            services.AddScoped(typeof(IRolRepository), typeof(RolRepository));

            services.AddScoped(typeof(IAreaRepository), typeof(AreaRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IDiscoveryRepository), typeof(DiscoveryRepository));
            services.AddScoped(typeof(ICurrentToolRepository), typeof(CurrentToolRepository));
            services.AddScoped(typeof(IProblemDetailsRepository), typeof(ProblemDetailsRepository));

            services.AddScoped(typeof(IFrecuencyRepository), typeof(FrecuencyRepository));
            services.AddScoped(typeof(IProblemRepository), typeof(ProblemRepository));
            services.AddScoped(typeof(ISectorRepository), typeof(SectorRepository));
            services.AddScoped(typeof(IToolRepository), typeof(ToolRepository));

            services.AddScoped(typeof(IBenefitRepository), typeof(BenefitRepository));
            services.AddScoped(typeof(IExpectedBenefitRepository), typeof(ExpectedBenefitRepository));
            services.AddScoped(typeof(IPromptRepository), typeof(PromptRepository));
            services.AddScoped(typeof(IConnectToolRepository), typeof(ConnectToolRepository));
            services.AddScoped(typeof(IUserMigrationRepository), typeof(UserMigrationRepository));
            services.AddScoped(typeof(IRepositoryMigrationRepository), typeof(RepositoryMigrationRepository));
            services.AddScoped(typeof(IRepositoryAnalizeGenAIRepository), typeof(RepositoryAnalizeGenAIRepository));
            services.AddScoped(typeof(IPackageMigrationRepository), typeof(PackageMigrationRepository));
            services.AddScoped(typeof(ISecurityMigrationRepository), typeof(SecurityMigrationRepository));
            services.AddScoped(typeof(IPipelineMigrationRepository), typeof(PipelineMigrationRepository));

            services.AddScoped(typeof(IEstimationOverviewRepository), typeof(EstimationOverviewRepository));
            services.AddScoped(typeof(IGithubIntegrations), typeof(GithubIntegrations));

            return services;
        }
    }
}
