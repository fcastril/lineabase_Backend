using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class UserMigrationService : BaseServiceApplication<UserMigration, UserMigrationDto>, IUserMigrationService
    {
        public UserMigrationService(IUserMigrationRepository userMigrationRepository): base(userMigrationRepository)
        {
            CreateMapperExpresion<UserMigration, UserMigrationDto>(cnf =>
            {
                UserMigrationMapper.Expresion(cnf);
            });
        }
    }
}