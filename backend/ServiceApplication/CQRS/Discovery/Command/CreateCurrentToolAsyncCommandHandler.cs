using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record CreateCurrentToolAsyncCommand(CurrentToolDto CurrentToolDto) : IRequest<CurrentToolDto>;
    public class CreateCurrentToolAsyncCommandHandler : IRequestHandler<CreateCurrentToolAsyncCommand, CurrentToolDto>
    {
        private readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;
        private readonly IBaseServiceApplication<Domain.Entities.Discovery, DiscoveryDto> _discoveryService;

        public CreateCurrentToolAsyncCommandHandler(
            IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation,
            IBaseServiceApplication<Domain.Entities.Discovery, DiscoveryDto> discoveryService)
        {
            _implementation = implementation;
           _discoveryService = discoveryService;
        }

        public async Task<CurrentToolDto> Handle(CreateCurrentToolAsyncCommand request, CancellationToken cancellationToken)
        {

            DiscoveryDto discoveryDto = await _discoveryService.FirstOrDefautlModelBy(x => x.Id == request.CurrentToolDto.DiscoveryId);

            request.CurrentToolDto.Discovery = discoveryDto;

            return await _implementation.CreateModel(request.CurrentToolDto);
        }
    }
}
