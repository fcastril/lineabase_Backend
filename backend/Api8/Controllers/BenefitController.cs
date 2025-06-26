using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using Util.Common;
using Utilidades;

namespace Api8.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class BenefitController : HandlerBaseController<Benefit, BenefitDto>
    {
        public BenefitController(IMediator mediator, IValidator<BenefitDto> validator) : base(validator, mediator)
        {

        }

        [HttpPost("paginator/{discoveryId}")]
        public async Task<IActionResult> PaginatorDiscovery(string discoveryId, [FromBody] Paginate<BenefitDto> paginado) => this.HandlerResponse(await _mediator.Send(new PaginateBenefitAsyncQuery(paginado, discoveryId)));

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> ToListDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new ToListBenefitByDiscoveryIdAsyncQuery(id)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteBenefitsByDiscoveryIdAsyncCommand(id)));
    }
}
