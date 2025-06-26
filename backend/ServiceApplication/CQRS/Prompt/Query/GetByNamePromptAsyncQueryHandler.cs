using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByNamePromptAsyncQuery(string name) : IRequest<PromptDto>;

    public class GetByNamePromptAsyncQueryHandler : IRequestHandler<GetByNamePromptAsyncQuery, PromptDto>
    {
        protected readonly IBaseServiceApplication<Prompt, PromptDto> _implementation;

        public GetByNamePromptAsyncQueryHandler(IBaseServiceApplication<Prompt, PromptDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<PromptDto> Handle(GetByNamePromptAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.Name == request.name);
        }
    }
}

