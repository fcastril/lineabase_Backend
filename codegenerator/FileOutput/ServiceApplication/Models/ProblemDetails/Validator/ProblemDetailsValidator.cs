
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class ProblemDetailsValidator : AbstractValidator<ProblemDetailsDto>
    {
		private readonly IProblemDetailsRepository _problemdetailsRepository;
        public ProblemDetailsValidator(IProblemDetailsRepository problemdetailsRepository)
        {
            _problemdetailsRepository = problemdetailsRepository;
            
        }
        
    }
}