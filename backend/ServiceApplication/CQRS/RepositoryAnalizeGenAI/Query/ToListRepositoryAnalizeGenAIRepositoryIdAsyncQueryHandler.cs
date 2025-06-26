using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery(string id) : IRequest<List<RepositoryAnalizeGenAIDto>>;

    public class ToListRepositoryAnalizeGenAIRepositoryIdAsyncQueryHandler : IRequestHandler<ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery, List<RepositoryAnalizeGenAIDto>>
    {
        protected readonly IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> _implementation;

        public ToListRepositoryAnalizeGenAIRepositoryIdAsyncQueryHandler(IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<RepositoryAnalizeGenAIDto>> Handle(ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.RepositoryId == request.id);
        }
    }
}

