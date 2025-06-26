using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class PipelineMigrationService : BaseServiceApplication<PipelineMigration, PipelineMigrationDto>, IPipelineMigrationService
    {
        public PipelineMigrationService(IPipelineMigrationRepository pipelineMigrationRepository): base(pipelineMigrationRepository) 
        {
            CreateMapperExpresion<PipelineMigration, PipelineMigrationDto>(
                cnf =>
                {
                    PipelineMigrationMapper.Expresion(cnf);
                });
        }
    }
}
