using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class DiscoveryService : BaseServiceApplication<Discovery, DiscoveryDto>, IDiscoveryService
    {
        public DiscoveryService(IDiscoveryRepository discoveryRepository,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService
            ) : base(discoveryRepository)
        {
            CreateMapperExpresion<Discovery, DiscoveryDto>(cnf =>
            {
                DiscoveryMapper.Expresion(cnf, customerService, sectorService);
            });
        }

    }
}