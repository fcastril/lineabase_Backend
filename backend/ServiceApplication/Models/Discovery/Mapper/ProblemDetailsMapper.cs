
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class ProblemDetailsMapper
    {

        public static void Expresion(
            IMapperConfigurationExpression cnf,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService,
            IBaseServiceApplication<Discovery, DiscoveryDto> discoveryService,
            IBaseServiceApplication<Category, CategoryDto> categoryService,
            IBaseServiceApplication<Problem, ProblemDto> problemService,
            IBaseServiceApplication<Frecuency, FrecuencyDto> frecuencyService
            )
        {

            DiscoveryMapper.Expresion(cnf, customerService, sectorService);
            CategoryMapper.Expresion(cnf);
            ProblemMapper.Expresion(cnf);
            FrecuencyMapper.Expresion(cnf);



            cnf.CreateMap<ProblemDetailsDto, ProblemDetails>()
                .ConstructUsing(src => src != null ? new ProblemDetails(
                    discoveryService.MapToENT<Discovery, DiscoveryDto>(src.Discovery),
                    categoryService.MapToENT<Category, CategoryDto>(src.Category),
                    problemService.MapToENT<Problem, ProblemDto>(src.Problem),
                    src.Impact,
                    frecuencyService.MapToENT<Frecuency, FrecuencyDto>(src.Frecuency),
                    src.Description) : null);
            cnf.CreateMap<ProblemDetails, ProblemDetailsDto>()
                .ForMember(dest => dest.Discovery, opt => opt.MapFrom(src => src.Discovery))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact))
                .ForMember(dest => dest.Frecuency, opt => opt.MapFrom(src => src.Frecuency))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}