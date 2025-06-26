using MediatR;
using Api.Base;
using Utilidades;
using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
  [Route(Constants.UriForDefaultWebApi + "[controller]")]
  [ApiController]
  public class RolController : HandlerBaseController<Rol, RolDto> {
    public RolController(IMediator mediator, IValidator<RolDto> validator) : base(validator, mediator) {

    }
  }
}
