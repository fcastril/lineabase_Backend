
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class ToolValidator : AbstractValidator<ToolDto>
    {
		private readonly IToolRepository _toolRepository;
        public ToolValidator(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;
            
        }
        
    }
}