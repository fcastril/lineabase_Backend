using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record UpdateCurrentToolAsyncCommand(CurrentToolDto CurrentToolDto) : IRequest<CurrentToolDto>;
    public class UpdateCurrentToolAsyncCommandHandler : IRequestHandler<UpdateCurrentToolAsyncCommand, CurrentToolDto>
    {
        private readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;
        private readonly IBaseServiceApplication<Domain.Entities.Discovery, DiscoveryDto> _discoveryService;

        public UpdateCurrentToolAsyncCommandHandler(
            IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation,
            IBaseServiceApplication<Domain.Entities.Discovery, DiscoveryDto> discoveryService)
        {
            _implementation = implementation;
           _discoveryService = discoveryService;
        }

        public async Task<CurrentToolDto> Handle(UpdateCurrentToolAsyncCommand request, CancellationToken cancellationToken)
        {

            DiscoveryDto discoveryDto = await _discoveryService.FirstOrDefautlModelBy(x => x.Id == request.CurrentToolDto.DiscoveryId);

            request.CurrentToolDto.Discovery = discoveryDto;

            return await _implementation.UpdateModel(request.CurrentToolDto);
        }
    }
}
