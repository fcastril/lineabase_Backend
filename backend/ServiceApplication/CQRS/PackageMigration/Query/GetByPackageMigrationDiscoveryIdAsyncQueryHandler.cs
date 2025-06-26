using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetByPackageMigrationDiscoveryIdAsyncQuery(string id) : IRequest<List<PackageMigrationDto>>;

    public class GetByPackageMigrationDiscoveryIdAsyncQueryHandler : IRequestHandler<GetByPackageMigrationDiscoveryIdAsyncQuery, List<PackageMigrationDto>>
    {
        protected readonly IBaseServiceApplication<PackageMigration, PackageMigrationDto> _implementation;

        public GetByPackageMigrationDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<PackageMigration, PackageMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<PackageMigrationDto>> Handle(GetByPackageMigrationDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

