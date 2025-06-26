using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class UserMigrationMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<UserMigrationDto, UserMigration>()
				.ConstructUsing(src => src != null ? new UserMigration(
                    src.DiscoveryId,
                    src.Name,
                    src.Email, 
                    src.Role,
                    src.Tool,
                    src.LastAccess) : null);
        }
    }
}