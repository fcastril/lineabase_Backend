using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class SecurityMigrationMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<SecurityMigrationDto, SecurityMigration>()
				.ConstructUsing(src => src != null ? new SecurityMigration(
                    src.DiscoveryId,
                    src.ProjectId,
                    src.Project,
                    src.TeamId,
                    src.Team,
                    src.User,
                    src.UserEmail) : null);
        }
    }
}