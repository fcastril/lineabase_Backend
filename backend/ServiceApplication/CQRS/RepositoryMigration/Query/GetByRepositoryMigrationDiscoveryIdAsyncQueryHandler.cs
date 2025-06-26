using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByRepositoryMigrationDiscoveryIdAsyncQuery(string id) : IRequest<List<RepositoryMigrationDto>>;

    public class GetByRepositoryMigrationDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByRepositoryMigrationDiscoveryIdAsyncQuery, List<RepositoryMigrationDto>>
    {
        protected readonly IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> _implementation;

        public GetByRepositoryMigrationDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<RepositoryMigrationDto>> Handle(GetByRepositoryMigrationDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

