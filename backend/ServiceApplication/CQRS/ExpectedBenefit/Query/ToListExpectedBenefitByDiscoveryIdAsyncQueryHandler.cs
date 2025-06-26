using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record ToListExpectedBenefitByDiscoveryIdAsyncQuery(string id) : IRequest<List<ExpectedBenefitDto>>;

    public class ToListExpectedBenefitByDiscoveryIdAsyncQueryHandler : IRequestHandler<ToListExpectedBenefitByDiscoveryIdAsyncQuery, List<ExpectedBenefitDto>>
    {
        protected readonly IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto> _implementation;

        public ToListExpectedBenefitByDiscoveryIdAsyncQueryHandler(IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<ExpectedBenefitDto>> Handle(ToListExpectedBenefitByDiscoveryIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistDtoBy(x => x.DiscoveryId == request.id);
        }
    }
}

