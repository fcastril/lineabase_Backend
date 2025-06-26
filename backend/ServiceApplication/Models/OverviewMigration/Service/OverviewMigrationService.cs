using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public class OverviewMigrationService : IOverviewMigrationService
    {
        private readonly IUserMigrationService _userMigrationService;
        private readonly IPackageMigrationService _packageMigrationService;
        private readonly IRepositoryMigrationService _repositoryMigrationService;
        private readonly IPipelineMigrationService _pipelineMigrationService;
        private readonly ISecurityMigrationService _securityMigrationService;

        public OverviewMigrationService(IUserMigrationService userMigrationService, IRepositoryMigrationService repositoryMigrationService, IPackageMigrationService packageMigrationService, IPipelineMigrationService pipelineMigrationService, ISecurityMigrationService securityMigrationService)
        {
            _packageMigrationService = packageMigrationService;
            _userMigrationService = userMigrationService;
            _repositoryMigrationService = repositoryMigrationService;
            _pipelineMigrationService = pipelineMigrationService;
            _securityMigrationService = securityMigrationService;
        }

        public async Task<List<OverviewMigrationDto>> GetOverview(string discoveryId)
        {
            return new()
            {
                new() 
                {
                    ResourceCategory = "Users",
                    Count = await _userMigrationService.Count(x => x.DiscoveryId == discoveryId)
                },
                new()
                {
                    ResourceCategory = "Repositories",
                    Count = await _repositoryMigrationService.Count(x => x.DiscoveryId == discoveryId)
                },
                new()
                {
                    ResourceCategory = "Security",
                    Count = await _securityMigrationService.Count(x => x.DiscoveryId == discoveryId)
                },
                new()
                {
                    ResourceCategory = "Packages",
                    Count = await _packageMigrationService.Count(x => x.DiscoveryId == discoveryId)
                },
                new()
                {
                    ResourceCategory = "Pipelines",
                    Count = await _pipelineMigrationService.Count(x => x.DiscoveryId == discoveryId)
                }
            };
        }
    }
}