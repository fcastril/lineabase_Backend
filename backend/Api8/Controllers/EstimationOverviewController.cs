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
    public class EstimationOverviewController : HandlerBaseController<EstimationOverview, EstimationOverviewDto>
    {
        public EstimationOverviewController(IValidator<EstimationOverviewDto> validator, IMediator mediator) : base(validator, mediator)
        {
        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> GetByDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new GetByEstimationOverviewDiscoveryIdAsyncQuery(id)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteEstimationOverviewByDiscoveryIdAsyncCommand(id)));
    }
}
