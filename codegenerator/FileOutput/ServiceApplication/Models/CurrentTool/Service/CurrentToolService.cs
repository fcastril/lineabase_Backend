
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class CurrentToolService : BaseServiceApplication<CurrentTool,CurrentToolDto>, ICurrentToolService
    {

        public CurrentToolService(ICurrentToolRepository currenttoolRepository): base(currenttoolRepository)
        {
            
            
            CreateMapperExpresion<CurrentTool, CurrentToolDto>(cnf =>
            {
                CurrentToolMapper.Expresion(cnf);
            });
        }
        
    }
}