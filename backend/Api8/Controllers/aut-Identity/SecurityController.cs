using MediatR;
using Api.Base;
using Utilidades;
using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;
using ServiceApplication.CQRS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Entity;

namespace Api.Controllers {
  /// <summary>
  /// 
  /// </summary>
  [Route(Constants.UriForDefaultWebApi + "[controller]")]
  [ApiController]
  public class SecurityController : HandlerBaseController<User, UserDto> {

    /// <summary>
    /// Security Controller
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="validator"></param>
    public SecurityController(IMediator mediator, IValidator<UserDto> validator) : base(validator, mediator) {

    }

    /// <summary>
    /// Login and Authentication
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> InicialSesion(Login login) => this.HandlerResponse(await _mediator.Send(new LoginAsyncQuery(login)));


  }
}
