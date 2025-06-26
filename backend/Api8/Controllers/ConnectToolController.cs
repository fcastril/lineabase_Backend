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
    public class ConnectToolController : HandlerBaseController<ConnectTool, ConnectToolDto>
    {
        public ConnectToolController(IMediator mediator, IValidator<ConnectToolDto> validator) : base(validator, mediator)
        {
            
        }

        [HttpPost("ValidateAndCreate")]
        public async Task<IActionResult> ValidateAndCreate([FromBody] ConnectToolDto connectToolDto)
        {
            var conn = await _mediator.Send(new GetConnectToolByDiscoveryIdAsyncQuery(connectToolDto.DiscoveryId));

            if (conn == null)
            {
                return await Create(connectToolDto);
            }

            return await Update(connectToolDto);
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetConnectToolByDiscoveryIdAsyncQuery(id)));
    }   
}
