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
    public class ElevatorMovementController : HandlerBaseController<ElevatorMovement, ElevatorMovementDto>
    {
        public ElevatorMovementController(IValidator<ElevatorMovementDto> validator , IMediator mediator)
            : base(validator, mediator)
        {

        }

    }
}
