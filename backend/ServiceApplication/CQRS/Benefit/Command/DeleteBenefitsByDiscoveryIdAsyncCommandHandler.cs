using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record DeleteBenefitsByDiscoveryIdAsyncCommand(string id) : IRequest<bool>;
    public class DeleteBenefitsByDiscoveryIdAsyncCommandHandler : IRequestHandler<DeleteBenefitsByDiscoveryIdAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<Benefit, BenefitDto> _implementation;

        public DeleteBenefitsByDiscoveryIdAsyncCommandHandler(
            IBaseServiceApplication<Benefit, BenefitDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteBenefitsByDiscoveryIdAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteAllModels(x => x.DiscoveryId == request.id);
        }
    }
}
