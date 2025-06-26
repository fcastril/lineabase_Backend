
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class AreaService : BaseServiceApplication<Area,AreaDto>, IAreaService
    {

        public AreaService(IAreaRepository areaRepository): base(areaRepository)
        {
            
            
            CreateMapperExpresion<Area, AreaDto>(cnf =>
            {
                AreaMapper.Expresion(cnf);
            });
        }
        
    }
}