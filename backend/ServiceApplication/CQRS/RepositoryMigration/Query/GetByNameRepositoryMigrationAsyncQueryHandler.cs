using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByNameRepositoryMigrationAsyncQuery(string name) : IRequest<RepositoryMigrationDto>;

    public class GetByNameRepositoryMigrationAsyncQueryHandler : IRequestHandler<GetByNameRepositoryMigrationAsyncQuery, RepositoryMigrationDto>
    {
        protected readonly IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> _implementation;

        public GetByNameRepositoryMigrationAsyncQueryHandler(IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<RepositoryMigrationDto> Handle(GetByNameRepositoryMigrationAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.Name.ToLower().Contains(request.name.ToLower()));
        }
    }
}

