using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication.CQRS
{

    public record PaginateCurrentToolAsyncQuery(Paginate<CurrentToolDto> paginado, string discoveryId) : IRequest<Paginate<CurrentToolDto>>;

    public class PaginateCurrentToolAsyncQueryHandler: IRequestHandler<PaginateCurrentToolAsyncQuery, Paginate<CurrentToolDto>>
    {
        protected readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;

        public PaginateCurrentToolAsyncQueryHandler(IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<Paginate<CurrentToolDto>> Handle(PaginateCurrentToolAsyncQuery request, CancellationToken cancellationToken)
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

