using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByIdCurrentToolAsyncQuery(string id) : IRequest<CurrentToolDto>;

    public class GetByIdCurrentToolAsyncQueryHandler : IRequestHandler<GetByIdCurrentToolAsyncQuery, CurrentToolDto>
    {
        protected readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;

        public GetByIdCurrentToolAsyncQueryHandler(IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<CurrentToolDto> Handle(GetByIdCurrentToolAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.Id == request.id);
        }
    }
}

