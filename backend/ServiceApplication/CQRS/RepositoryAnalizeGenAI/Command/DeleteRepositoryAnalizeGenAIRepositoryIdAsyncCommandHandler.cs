using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 
    public record DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteRepositoryAnalizeGenAIByRepositoryIdAsyncCommandHandler : IRequestHandler<DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> _implementation;

        public DeleteRepositoryAnalizeGenAIByRepositoryIdAsyncCommandHandler(
            IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.RepositoryId == request.id);
        }
    }
}
