using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication.CQRS
{

    public record PaginateProblemDetailsAsyncQuery(Paginate<ProblemDetailsDto> paginado, string discoveryId) : IRequest<Paginate<ProblemDetailsDto>>;

    public class PaginateProblemDetailsAsyncQueryHandler : IRequestHandler<PaginateProblemDetailsAsyncQuery, Paginate<ProblemDetailsDto>>
    {
        protected readonly IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> _implementation;

        public PaginateProblemDetailsAsyncQueryHandler(IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<Paginate<ProblemDetailsDto>> Handle(PaginateProblemDetailsAsyncQuery request, CancellationToken cancellationToken)
        {

            request.paginado.FiltersPaginate = new();
            request.paginado.FiltersPaginate.Add(new FilterPaginate()
            {
                Property = "DiscoveryId",
                Value = request.discoveryId
            });
            

            return await _implementation.Paginate(request.paginado);
        }
    }
}

