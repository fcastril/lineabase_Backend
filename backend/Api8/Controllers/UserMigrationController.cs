using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Util.Common;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class UserMigrationController : HandlerBaseController<UserMigration, UserMigrationDto>
    {
        public UserMigrationController(IMediator mediator, IValidator<UserMigrationDto> validator) : base(validator, mediator)
        {
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByUserMigrationDiscoveryIdAsyncQuery(id)));

        [HttpGet("GetByFilterNameEmail/{search}")]
        public async Task<IActionResult> GetFilterNameEmail(string search) => this.HandlerResponse(await _mediator.Send(new FilteredNameEmailUserMigrationQuery(search)));

        [HttpGet("GetByFilterRoleInactivity/{role}/{inactivity}")]
        public async Task<IActionResult> GetFilterRolaInactivity(string role, int inactivity) => this.HandlerResponse(await _mediator.Send(new FilteredRoleActivityUserMigrationQuery(role, inactivity)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteUserMigrationByDiscoveryIdAsyncCommand(id)));
    }
}