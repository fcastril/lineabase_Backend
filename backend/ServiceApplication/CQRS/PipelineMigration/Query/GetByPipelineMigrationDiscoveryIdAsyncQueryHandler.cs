using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetByPipelineMigrationDiscoveryIdAsyncQuery(string id) : IRequest<List<PipelineMigrationDto>>;

    public class GetByPipelineMigrationDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByPipelineMigrationDiscoveryIdAsyncQuery, List<PipelineMigrationDto>>
    {
        protected readonly IBaseServiceApplication<PipelineMigration, PipelineMigrationDto> _implementation;

        public GetByPipelineMigrationDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<PipelineMigration, PipelineMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<PipelineMigrationDto>> Handle(GetByPipelineMigrationDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}
