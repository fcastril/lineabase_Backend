
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class ProblemValidator : AbstractValidator<ProblemDto>
    {
		private readonly IProblemRepository _problemRepository;
        public ProblemValidator(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
            
        }
        
    }
}