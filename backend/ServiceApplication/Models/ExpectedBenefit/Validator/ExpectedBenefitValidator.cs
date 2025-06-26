using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class ExpectedBenefitValidator : AbstractValidator<ExpectedBenefitDto>
    {
        private readonly IExpectedBenefitRepository _expectedBenefitRepository;
        public ExpectedBenefitValidator(IExpectedBenefitRepository expectedBenefitRepository)
        {
            _expectedBenefitRepository = expectedBenefitRepository;

        }
    }
}
