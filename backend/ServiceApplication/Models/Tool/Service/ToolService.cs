
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class ToolService : BaseServiceApplication<Tool,ToolDto>, IToolService
    {

        public ToolService(IToolRepository toolRepository): base(toolRepository)
        {
            
            
            CreateMapperExpresion<Tool, ToolDto>(cnf =>
            {
                ToolMapper.Expresion(cnf);
            });
        }
        
    }
}