using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record DeleteExpectedBenefitsByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteExpectedBenefitsByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteExpectedBenefitsByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto> _implementation;

        public DeleteExpectedBenefitsByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteExpectedBenefitsByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
