using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class PackageMigrationMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<PackageMigrationDto, PackageMigration>()
				.ConstructUsing(src => src != null ? new PackageMigration(
                    src.DiscoveryId,
                    src.Name,
                    src.Type,
                    src.Version,
                    src.IsDeleted,
                    src.LastUpdate,
                    src.Project,
                    src.ProjectId) : null);
        }
    }
}