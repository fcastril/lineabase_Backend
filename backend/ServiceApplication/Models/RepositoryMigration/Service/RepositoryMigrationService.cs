using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class RepositoryMigrationService : BaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>, IRepositoryMigrationService
    {
        public RepositoryMigrationService(IRepositoryMigrationRepository repositoryMigrationRepository): base(repositoryMigrationRepository)
        {
            CreateMapperExpresion<RepositoryMigration, RepositoryMigrationDto>(cnf =>
            {
                RepositoryMigrationMapper.Expresion(cnf);
            });
        }
    }
}