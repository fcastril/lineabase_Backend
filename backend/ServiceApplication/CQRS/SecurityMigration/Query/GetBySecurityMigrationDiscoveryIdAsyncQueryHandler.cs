using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetBySecurityMigrationDiscoveryIdAsyncQuery(string id) : IRequest<List<SecurityMigrationDto>>;

    public class GetBySecurityMigrationDiscoveryIdAsyncQueryHandler : IRequestHandler<GetBySecurityMigrationDiscoveryIdAsyncQuery, List<SecurityMigrationDto>>
    {
        protected readonly IBaseServiceApplication<SecurityMigration, SecurityMigrationDto> _implementation;

        public GetBySecurityMigrationDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<SecurityMigration, SecurityMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<SecurityMigrationDto>> Handle(GetBySecurityMigrationDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

