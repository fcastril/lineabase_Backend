
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class FrecuencyService : BaseServiceApplication<Frecuency,FrecuencyDto>, IFrecuencyService
    {

        public FrecuencyService(IFrecuencyRepository frecuencyRepository): base(frecuencyRepository)
        {
            
            
            CreateMapperExpresion<Frecuency, FrecuencyDto>(cnf =>
            {
                FrecuencyMapper.Expresion(cnf);
            });
        }
        
    }
}