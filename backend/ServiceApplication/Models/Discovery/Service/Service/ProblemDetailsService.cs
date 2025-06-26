using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class ProblemDetailsService : BaseServiceApplication<ProblemDetails, ProblemDetailsDto>, IProblemDetailsService
    {

        public ProblemDetailsService(
            IProblemDetailsRepository problemdetailsRepository,
            IBaseServiceApplication<Customer, CustomerDto> customerService,
            IBaseServiceApplication<Sector, SectorDto> sectorService,
            IBaseServiceApplication<Discovery, DiscoveryDto> discoveryService,
            IBaseServiceApplication<Category, CategoryDto> categoryService,
            IBaseServiceApplication<Problem, ProblemDto> problemService,
            IBaseServiceApplication<Frecuency, FrecuencyDto> frecuencyService

            ) : base(problemdetailsRepository)
        {


            CreateMapperExpresion<ProblemDetails, ProblemDetailsDto>(cnf =>
            {
                ProblemDetailsMapper.Expresion(cnf, customerService, sectorService, discoveryService, categoryService, problemService, frecuencyService);
            });
        }

    }
}