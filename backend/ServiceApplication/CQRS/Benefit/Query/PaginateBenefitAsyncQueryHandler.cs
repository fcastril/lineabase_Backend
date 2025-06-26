using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication.CQRS
{

    public record PaginateBenefitAsyncQuery(Paginate<BenefitDto> paginado, string discoveryId) : IRequest<Paginate<BenefitDto>>;

    public class PaginateBenefitAsyncQueryHandler: IRequestHandler<PaginateBenefitAsyncQuery, Paginate<BenefitDto>>
    {
        protected readonly IBaseServiceApplication<Benefit, BenefitDto> _implementation;

        public PaginateBenefitAsyncQueryHandler(IBaseServiceApplication<Benefit, BenefitDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<Paginate<BenefitDto>> Handle(PaginateBenefitAsyncQuery request, CancellationToken cancellationToken)
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

