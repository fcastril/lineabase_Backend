using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetConnectToolByDiscoveryIdAsyncQuery(string id) : IRequest<ConnectToolDto>;

    public class GetConnectToolByDiscoveryIdAsyncQueryHandler : IRequestHandler<GetConnectToolByDiscoveryIdAsyncQuery, ConnectToolDto>
    {
        protected readonly IBaseServiceApplication<ConnectTool, ConnectToolDto> _implementation;

        public GetConnectToolByDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<ConnectTool, ConnectToolDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<ConnectToolDto> Handle(GetConnectToolByDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.DiscoveryId == request.id);
        }
    }
}

