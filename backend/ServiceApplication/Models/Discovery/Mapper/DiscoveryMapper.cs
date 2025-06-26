using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class DiscoveryMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService)
        {
            CustomerMapper.Expresion(cnf, sectorService);

            cnf.AllowNullCollections = true;
            cnf.AllowNullDestinationValues = true;

            cnf.CreateMap<DiscoveryDto, Discovery>()
                .ConstructUsing(src => src != null ? new Discovery(
                    customerService.MapToENT<Customer, CustomerDto>(src.Customer),
                    src.Date.Value, src.Description
                    ) : new());
            cnf.CreateMap<Discovery, DiscoveryDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer ?? new Customer()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date ?? System.DateTimeOffset.MinValue))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StatusBenefit, opt => opt.MapFrom(src => src.StatusBenefit));
        }
    }
}