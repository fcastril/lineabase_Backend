using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByUserMigrationDiscoveryIdAsyncQuery(string id) : IRequest<List<UserMigrationDto>>;

    public class GetByUserMigrationDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByUserMigrationDiscoveryIdAsyncQuery, List<UserMigrationDto>>
    {
        protected readonly IBaseServiceApplication<UserMigration, UserMigrationDto> _implementation;

        public GetByUserMigrationDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<UserMigration, UserMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<UserMigrationDto>> Handle(GetByUserMigrationDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

