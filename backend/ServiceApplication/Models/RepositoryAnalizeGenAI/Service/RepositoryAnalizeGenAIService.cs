using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class RepositoryAnalizeGenAIService : BaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>, IRepositoryAnalizeGenAIService
    {
        public RepositoryAnalizeGenAIService(IRepositoryAnalizeGenAIRepository repositoryAnalizeGenAIRepository): base(repositoryAnalizeGenAIRepository)
        {
            CreateMapperExpresion<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>(cnf =>
            {
                RepositoryAnalizeGenAIMapper.Expresion(cnf);
            });
        }
    }
}