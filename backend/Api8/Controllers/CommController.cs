using Api.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.Events;
using ServiceApplication.Port;

namespace Api8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommController :  HandlerBaseLiteController<CommGeneric>
    {
        private readonly IMessageSender<CommGeneric> _messageSender;
        public CommController(IMediator mediator, IMessageSender<CommGeneric> messageSender)
            : base(mediator)
        {
            _messageSender = messageSender;
        }

        [HttpPost("SendMessageToQueue")]
        public async Task<IActionResult> SendMessageToQueue([FromBody] CommGeneric commGeneric)
        {
            await _messageSender.SendCommAsync(commGeneric);

            return Ok();
        }
    }
}