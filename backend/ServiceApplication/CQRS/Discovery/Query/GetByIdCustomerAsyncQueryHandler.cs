using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetByIdCustomerAsyncQuery(string id) : IRequest<DiscoveryDto>;

    public class GetByIdCustomerAsyncQueryHandler : IRequestHandler<GetByIdCustomerAsyncQuery, DiscoveryDto>
    {
        protected readonly IBaseServiceApplication<Discovery, DiscoveryDto> _implementation;

        public GetByIdCustomerAsyncQueryHandler(IBaseServiceApplication<Discovery, DiscoveryDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<DiscoveryDto> Handle(GetByIdCustomerAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.Customer.Id == request.id);
        }
    }
}

