using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetByEstimationOverviewDiscoveryIdAsyncQuery(string id) : IRequest<List<EstimationOverviewDto>>;

    public class GetByEstimationOverviewDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByEstimationOverviewDiscoveryIdAsyncQuery, List<EstimationOverviewDto>>
    {
        protected readonly IBaseServiceApplication<EstimationOverview, EstimationOverviewDto> _implementation;

        public GetByEstimationOverviewDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<EstimationOverview, EstimationOverviewDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<EstimationOverviewDto>> Handle(GetByEstimationOverviewDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

