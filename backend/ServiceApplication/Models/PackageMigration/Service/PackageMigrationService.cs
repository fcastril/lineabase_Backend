using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class PackageMigrationService : BaseServiceApplication<PackageMigration, PackageMigrationDto>, IPackageMigrationService
    {
        public PackageMigrationService(IPackageMigrationRepository packageMigrationRepository): base(packageMigrationRepository)
        {
            CreateMapperExpresion<PackageMigration, PackageMigrationDto>(cnf =>
            {
                PackageMigrationMapper.Expresion(cnf);
            });
        }
    }
}