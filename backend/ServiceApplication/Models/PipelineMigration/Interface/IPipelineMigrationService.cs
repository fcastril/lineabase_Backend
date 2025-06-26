using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication
{
    public interface IPipelineMigrationService : IBaseServiceApplication<PipelineMigration, PipelineMigrationDto>
    {
    }
}
