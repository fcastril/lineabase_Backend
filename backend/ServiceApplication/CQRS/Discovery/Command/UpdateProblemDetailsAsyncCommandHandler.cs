using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record UpdateProblemDetailsAsyncCommand(ProblemDetailsDto ProblemDetailsDto) : IRequest<ProblemDetailsDto>;
    public class UpdateProblemDetailsAsyncCommandHandler : IRequestHandler<UpdateProblemDetailsAsyncCommand, ProblemDetailsDto>
    {
        private readonly IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> _implementation;
        private readonly IBaseServiceApplication<Discovery, DiscoveryDto> _discoveryService;

        public UpdateProblemDetailsAsyncCommandHandler(
            IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> implementation,
            IBaseServiceApplication<Discovery, DiscoveryDto> discoveryService)
        {
            _implementation = implementation;
           _discoveryService = discoveryService;
        }

        public async Task<ProblemDetailsDto> Handle(UpdateProblemDetailsAsyncCommand request, CancellationToken cancellationToken)
        {

            DiscoveryDto discoveryDto = await _discoveryService.FirstOrDefautlModelBy(x => x.Id == request.ProblemDetailsDto.DiscoveryId);

            request.ProblemDetailsDto.Discovery = discoveryDto;

            return await _implementation.UpdateModel(request.ProblemDetailsDto);
        }
    }
}
