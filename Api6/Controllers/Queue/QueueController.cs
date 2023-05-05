using Api.Base;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Models.Queue;
using ServiceBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus.Listener;
using Util.Common;
using Utilidades;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api6.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]

    public class QueueController : HandlerBaseLiteController<EventQueue>
    {
        private readonly IMediator mediator;
        private readonly IServicesBusHandler _servicesBusHandler;
        private readonly IElevatorMovementService _elevatorService;
        private readonly IUtil _util;

        public QueueController(IMediator mediator,IServicesBusHandler servicesBusHandler,
            IElevatorMovementService elevatorService,Util.Common.IUtil util) : base(mediator)
        {
            this.mediator = mediator;
            this._servicesBusHandler = servicesBusHandler;
            _elevatorService = elevatorService;
            _util = util;
        }

        [HttpPost("sendEventHighPriority")]
        public async Task<IActionResult> SendEventHP(QueueInputDto eventQueue)
        {
            var rest =await _elevatorService.CreateModel(new ServiceApplication.Dto.ElevatorMovementDto()
            {
                ElevatorCode = _util.GetHeaderRequest(EHeaders.CodeClient),
                Priority = 1,
                Floor = eventQueue.Floor,
                Status= States.Active.ToString(),
                Code=Guid.NewGuid().ToString()
            }) ; ;
            await _servicesBusHandler.SendMessageQueue(new EventQueue() {Floor= eventQueue.Floor ,Priority=1,CodeElevator = rest.ElevatorCode, CodeMovement = rest.Code }, "queuehighpriority");
            return HandlerResponse(eventQueue);
        }

        [HttpPost("sendEventLowPriority")]
        public async Task<IActionResult> SendEventLP(QueueInputDto eventQueue)
        {
            var rest=await _elevatorService.CreateModel(new ServiceApplication.Dto.ElevatorMovementDto()
            {
                ElevatorCode = _util.GetHeaderRequest(EHeaders.CodeClient),
                Priority = 2,
                Floor = eventQueue.Floor,
                Status = States.Active.ToString(),
                Code = Guid.NewGuid().ToString()
            });
            await _servicesBusHandler.SendMessageQueue(new EventQueue() { Floor = eventQueue.Floor, Priority = 2 ,CodeElevator=rest.ElevatorCode,CodeMovement= rest.Code}, "queuelowpriority");
            return HandlerResponse(eventQueue);
        }
    }


}

