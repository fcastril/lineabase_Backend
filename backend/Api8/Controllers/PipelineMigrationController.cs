using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Utilidades;

namespace Api8.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class PipelineMigrationController : HandlerBaseController<PipelineMigration, PipelineMigrationDto>
    {
        public PipelineMigrationController(IMediator mediator, IValidator<PipelineMigrationDto> validator) : base(validator, mediator)
        {
            
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByPipelineMigrationDiscoveryIdAsyncQuery(id)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeletePipelineMigrationByDiscoveryIdAsyncCommand(id)));
    }
}
