using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQuery(string id) : IRequest<List<RepositoryAnalizeGenAIDto>>;

    public class GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQuery, List<RepositoryAnalizeGenAIDto>>
    {
        protected readonly IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> _implementation;

        public GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<RepositoryAnalizeGenAIDto>> Handle(GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

