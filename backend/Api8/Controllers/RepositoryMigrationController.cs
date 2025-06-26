using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class RepositoryMigrationController : HandlerBaseController<RepositoryMigration, RepositoryMigrationDto>
    {
        public RepositoryMigrationController(IMediator mediator, IValidator<RepositoryMigrationDto> validator) : base(validator, mediator)
        {
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByRepositoryMigrationDiscoveryIdAsyncQuery(id)));

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name) => this.HandlerResponse(await _mediator.Send(new GetByNameRepositoryMigrationAsyncQuery(name)));

        [HttpGet("GetByFilterStatusLastUpdate")]
        public async Task<IActionResult> GetFilterRolaInactivity([FromQuery] bool status, [FromQuery] DateTime lastUpdate) => this.HandlerResponse(await _mediator.Send(new FilterStatusLastUpdateRepositoryMigrationQuery(status, lastUpdate)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteRepositoryMigrationByDiscoveryIdAsyncCommand(id)));
    }
}
