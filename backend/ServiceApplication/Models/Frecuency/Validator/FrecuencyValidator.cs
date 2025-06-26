
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class FrecuencyValidator : AbstractValidator<FrecuencyDto>
    {
		private readonly IFrecuencyRepository _frecuencyRepository;
        public FrecuencyValidator(IFrecuencyRepository frecuencyRepository)
        {
            _frecuencyRepository = frecuencyRepository;
            
        }
        
    }
}