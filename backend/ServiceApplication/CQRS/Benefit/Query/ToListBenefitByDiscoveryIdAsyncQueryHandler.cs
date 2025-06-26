using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record ToListBenefitByDiscoveryIdAsyncQuery(string id) : IRequest<List<BenefitDto>>;

    public class ToListBenefitByDiscoveryIdAsyncQueryHandler : IRequestHandler<ToListBenefitByDiscoveryIdAsyncQuery, List<BenefitDto>>
    {
        protected readonly IBaseServiceApplication<Benefit, BenefitDto> _implementation;

        public ToListBenefitByDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<Benefit, BenefitDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<BenefitDto>> Handle(ToListBenefitByDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

