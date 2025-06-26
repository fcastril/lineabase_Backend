using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class PipelineMigrationValidator : AbstractValidator<PipelineMigrationDto>
    {
        private readonly IPipelineMigrationRepository _pipelineMigrationRepository;

        public PipelineMigrationValidator(IPipelineMigrationRepository pipelineMigrationRepository)
        {
            _pipelineMigrationRepository = pipelineMigrationRepository;
        }
    }
}
