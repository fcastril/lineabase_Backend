using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteSecurityMigrationByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteSecurityMigrationByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteSecurityMigrationByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<SecurityMigration, SecurityMigrationDto> _implementation;

        public DeleteSecurityMigrationByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<SecurityMigration, SecurityMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteSecurityMigrationByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
