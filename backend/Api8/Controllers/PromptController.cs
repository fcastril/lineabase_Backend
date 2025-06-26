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
    public class PromptController : HandlerBaseController<Prompt, PromptDto>
    {
        public PromptController(IMediator mediator, IValidator<PromptDto> validator) : base(validator, mediator)
        {
            
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name) => this.HandlerResponse(await _mediator.Send(new GetByNamePromptAsyncQuery(name)));
    }
}
