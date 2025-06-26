using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeletePackageMigrationByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeletePackageMigrationByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeletePackageMigrationByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<PackageMigration, PackageMigrationDto> _implementation;

        public DeletePackageMigrationByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<PackageMigration, PackageMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeletePackageMigrationByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
