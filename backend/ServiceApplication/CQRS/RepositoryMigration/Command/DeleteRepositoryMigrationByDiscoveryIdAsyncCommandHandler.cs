using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteRepositoryMigrationByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteRepositoryMigrationByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteRepositoryMigrationByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> _implementation;

        public DeleteRepositoryMigrationByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteRepositoryMigrationByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
