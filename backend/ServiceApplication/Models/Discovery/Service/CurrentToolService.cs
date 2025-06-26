using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class CurrentToolService : BaseServiceApplication<CurrentTool, CurrentToolDto>, ICurrentToolService
    {

        public CurrentToolService(
            ICurrentToolRepository currenttoolRepository,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService,
            IBaseServiceApplication<Discovery, DiscoveryDto> discoveryService,
            IBaseServiceApplication<Tool, ToolDto> toolService,
            IBaseServiceApplication<Area, AreaDto> areaService
            ) : base(currenttoolRepository)
        {


            CreateMapperExpresion<CurrentTool, CurrentToolDto>(cnf =>
            {
                CurrentToolMapper.Expresion(cnf,
                    customerService,
                    sectorService,
                    discoveryService,
                    toolService,
                    areaService
                    );
            });
        }

    }
}