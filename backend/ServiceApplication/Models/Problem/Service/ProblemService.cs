
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class ProblemService : BaseServiceApplication<Problem,ProblemDto>,IProblemService
    {

        public ProblemService(IProblemRepository problemRepository): base(problemRepository)
        {
            
            
            CreateMapperExpresion<Problem, ProblemDto>(cnf =>
            {
                ProblemMapper.Expresion(cnf);
            });
        }
        
    }
}