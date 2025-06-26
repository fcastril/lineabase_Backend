
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class ProblemDetailsService : BaseServiceApplication<ProblemDetails,ProblemDetailsDto>, IProblemDetailsService
    {

        public ProblemDetailsService(IProblemDetailsRepository problemdetailsRepository): base(problemdetailsRepository)
        {
            
            
            CreateMapperExpresion<ProblemDetails, ProblemDetailsDto>(cnf =>
            {
                ProblemDetailsMapper.Expresion(cnf);
            });
        }
        
    }
}