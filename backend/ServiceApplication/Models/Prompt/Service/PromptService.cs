using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class PromptService : BaseServiceApplication<Prompt, PromptDto>, IPromptService
    {
        public PromptService(IPromptRepository promptRepository): base(promptRepository)
        {
            CreateMapperExpresion<Prompt, PromptDto>(cnf =>
            {
                PromptMapper.Expresion(cnf);
            });
        }
    }
}