using Api.Base;
using Api6.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Utilidades;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route(ConstantsAPI.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class UserController : HandlerBaseController<User, UserDto>
    {
        public UserController(IValidator<UserDto> validator, IMediator mediator)
            : base(validator, mediator)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> InicialSesion(Login login) => this.HandlerResponse(await _mediator.Send(new LoginAsyncQuery(login)));

    }
}
