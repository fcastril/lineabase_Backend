using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record ToListProblemDetailsAsyncQuery(string discoveryId) : IRequest<List<ProblemDetailsDto>>;

    public class ToListProblemDetailsAsyncQueryHandler : IRequestHandler<ToListProblemDetailsAsyncQuery, List<ProblemDetailsDto>>
    {
        protected readonly IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> _implementation;

        public ToListProblemDetailsAsyncQueryHandler(IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<ProblemDetailsDto>> Handle(ToListProblemDetailsAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.ToListModelBy(x => x.Discovery.Id == request.discoveryId);
        }
    }
}

