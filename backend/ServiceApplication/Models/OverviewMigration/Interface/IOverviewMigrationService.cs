using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public interface IOverviewMigrationService 
    {
        Task<List<OverviewMigrationDto>> GetOverview(string discoveryId);
    }
}