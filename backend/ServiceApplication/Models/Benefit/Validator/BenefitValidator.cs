using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class BenefitValidator : AbstractValidator<BenefitDto>
    {
		private readonly IBenefitRepository _benefitRepository;
        public BenefitValidator(IBenefitRepository benefitRepository)
        {
            _benefitRepository = benefitRepository;
        }
    }
}