using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> _implementation;

        public DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
