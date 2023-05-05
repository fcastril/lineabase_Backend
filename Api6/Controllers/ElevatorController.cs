using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Util.Common;
using Utilidades;

namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class ElevatorController : HandlerBaseController<Elevator, ElevatorDto>
    {
        private readonly IUtil util;

        public ElevatorController(IValidator<ElevatorDto> validator , IMediator mediator,IUtil util)
            : base(validator, mediator)
        {
            this.util = util;
        }

        [HttpGet("getStatus")]
        public async Task<IActionResult> GetStatus()
        {
            return this.HandlerResponse(await _mediator.Send(new GetStatusQuery(util.GetHeaderRequest(EHeaders.CodeClient))));
        }

    }
}
