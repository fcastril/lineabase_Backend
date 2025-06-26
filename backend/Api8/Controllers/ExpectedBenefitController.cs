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
    public class ExpectedBenefitController : HandlerBaseController<ExpectedBenefit, ExpectedBenefitDto>
    {
        public ExpectedBenefitController(IMediator mediator, IValidator<ExpectedBenefitDto> validator) : base(validator, mediator)
        {

        }

        [HttpGet("Discovery/{id}")]
        public async Task<IActionResult> ToListDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new ToListExpectedBenefitByDiscoveryIdAsyncQuery(id)));

        [HttpDelete("Discovery/{id}")]
        public async Task<IActionResult> DeleteDiscoveryId(string id) => this.HandlerResponse(await _mediator.Send(new DeleteExpectedBenefitsByDiscoveryIdAsyncCommand(id)));
    }
}
