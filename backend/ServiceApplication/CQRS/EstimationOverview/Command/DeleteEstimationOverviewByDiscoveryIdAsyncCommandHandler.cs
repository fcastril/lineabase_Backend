using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteEstimationOverviewByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteEstimationOverviewByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteEstimationOverviewByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<EstimationOverview, EstimationOverviewDto> _implementation;

        public DeleteEstimationOverviewByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<EstimationOverview, EstimationOverviewDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteEstimationOverviewByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
