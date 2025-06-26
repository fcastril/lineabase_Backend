using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record ToListCurrentToolAsyncQuery(string discoveryId) : IRequest<List<CurrentToolDto>>;

    public class ToListCurrentToolAsyncQueryHandler : IRequestHandler<ToListCurrentToolAsyncQuery, List<CurrentToolDto>>
    {
        protected readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;

        public ToListCurrentToolAsyncQueryHandler(IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<CurrentToolDto>> Handle(ToListCurrentToolAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.ToListModelBy(x => x.Discovery.Id == request.discoveryId);
        }
    }
}

