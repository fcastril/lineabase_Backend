
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.Validator
{
    public class CurrentToolValidator : AbstractValidator<CurrentToolDto>
    {
        private readonly ICurrentToolRepository _currenttoolRepository;
        private readonly IDiscoveryService _discoveryService;

        public CurrentToolValidator(ICurrentToolRepository currenttoolRepository, IDiscoveryService discoveryService)
        {
            _currenttoolRepository = currenttoolRepository;
            _discoveryService = discoveryService;

            RuleFor(ct => ct.DiscoveryId)
                .NotEmpty().WithMessage("The Id of Discovery is required")
                .MustAsync(DiscoveryExists).WithMessage("The Discovery Entity not found").WithErrorCode("404");

        }

        private async Task<bool> DiscoveryExists(string discoveryId, CancellationToken cancellationToken)
        {
            DiscoveryDto discovery = await _discoveryService.FirstOrDefautlModelBy(x => x.Id == discoveryId);
            return discovery != null;

        }
    }
}