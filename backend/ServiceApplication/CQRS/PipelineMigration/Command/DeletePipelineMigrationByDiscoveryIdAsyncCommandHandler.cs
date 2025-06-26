using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeletePipelineMigrationByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeletePipelineMigrationByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeletePipelineMigrationByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<PipelineMigration, PipelineMigrationDto> _implementation;

        public DeletePipelineMigrationByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<PipelineMigration, PipelineMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeletePipelineMigrationByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
