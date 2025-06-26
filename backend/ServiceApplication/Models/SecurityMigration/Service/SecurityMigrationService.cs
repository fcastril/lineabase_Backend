using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class SecurityMigrationService : BaseServiceApplication<SecurityMigration, SecurityMigrationDto>, ISecurityMigrationService
    {
        public SecurityMigrationService(ISecurityMigrationRepository securityMigrationRepository): base(securityMigrationRepository)
        {
            CreateMapperExpresion<SecurityMigration, SecurityMigrationDto>(cnf =>
            {
                SecurityMigrationMapper.Expresion(cnf);
            });
        }
    }
}