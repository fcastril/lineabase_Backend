using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class RepositoryMigrationMapper
    {
        public static void Expresion(
            IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<RepositoryMigrationDto, RepositoryMigration>()
				.ConstructUsing(src => src != null ? new RepositoryMigration(
                    src.DiscoveryId,
                    src.Assessment, 
                    src.Name,
                    src.Description,
                    src.Size,
                    src.BranchCount,
                    src.ActivePullRequest,
                    src.LastCommit,
                    src.IsDisable,
                    src.DefaultBranch,
                    src.Branchs,
                    src.FrecuencyCommits,
                    src.FileExtensions) : null);
        }
    }
}