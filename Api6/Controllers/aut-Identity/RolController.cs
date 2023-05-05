using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication;
using ServiceApplication.Dto;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class RolController : HandlerBaseController<Rol, RolDto>
    {
        public RolController(IValidator<RolDto> validator, IMediator mediator)
            : base(validator, mediator)
        {

        }

    }
}
