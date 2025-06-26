
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class CurrentToolValidator : AbstractValidator<CurrentToolDto>
    {
		private readonly ICurrentToolRepository _currenttoolRepository;
        public CurrentToolValidator(ICurrentToolRepository currenttoolRepository)
        {
            _currenttoolRepository = currenttoolRepository;
            
        }
        
    }
}