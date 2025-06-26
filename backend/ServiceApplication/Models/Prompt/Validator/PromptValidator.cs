using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class PromptValidator : AbstractValidator<PromptDto>
    {
		private readonly IPromptRepository _promptRepository;

        public PromptValidator(IPromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }
    }
}