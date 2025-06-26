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
    public class PackageMigrationController : HandlerBaseController<PackageMigration, PackageMigrationDto>
    {
        public PackageMigrationController(IMediator mediator, IValidator<PackageMigrationDto> validator) : base(validator, mediator)
        {
            
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByPackageMigrationDiscoveryIdAsyncQuery(id)));

        [HttpGet("GetByFilter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string type = null, [FromQuery]  bool? isDeleted = null, [FromQuery] DateTime? lastUpdate = null) => this.HandlerResponse(await _mediator.Send(new FilterPackageMigrationQuery(type, isDeleted, lastUpdate)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeletePackageMigrationByDiscoveryIdAsyncCommand(id)));
    }
}
