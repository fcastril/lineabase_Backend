
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class SectorService : BaseServiceApplication<Sector,SectorDto>,ISectorService
    {

        public SectorService(ISectorRepository sectorRepository): base(sectorRepository)
        {
            
            
            CreateMapperExpresion<Sector, SectorDto>(cnf =>
            {
                SectorMapper.Expresion(cnf);
            });
        }
        
    }
}