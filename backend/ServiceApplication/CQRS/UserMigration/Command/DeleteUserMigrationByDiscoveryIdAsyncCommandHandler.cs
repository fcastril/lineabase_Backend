using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteUserMigrationByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteUserMigrationByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteUserMigrationByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<UserMigration, UserMigrationDto> _implementation;

        public DeleteUserMigrationByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<UserMigration, UserMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteUserMigrationByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
