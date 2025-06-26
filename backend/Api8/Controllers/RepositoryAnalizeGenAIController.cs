using Api.Base;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Port;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class RepositoryAnalizeGenAIController : HandlerBaseController<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>
    {
        private readonly IMessageSender<MessageQueue> _messageSender;

        public RepositoryAnalizeGenAIController(IValidator<RepositoryAnalizeGenAIDto> validator, IMediator mediator, IMessageSender<MessageQueue> messageSender) : base(validator, mediator)
        {
            _messageSender = messageSender;
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQuery(id)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommand(id)));

        [HttpGet("RepositoryMigration/{id}")]
        public async Task<IActionResult> ToListRepositoryId(string id) => this.HandlerResponse(await _mediator.Send(new ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery(id)));

        [HttpDelete("RepositoryMigration/{id}")]
        public async Task<IActionResult> DeleteRepositoryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand(id)));

        [HttpPost("SendMessageToQueue")]
        public async Task<IActionResult> SendMessageToQueue([FromBody] MessageQueue messageQueue)
        {
            messageQueue.Tool = Tools.GenAIRepository.ToString();
            await _messageSender.SendAsync(messageQueue);

            return this.HandlerResponse(true);
        }
    }
}
