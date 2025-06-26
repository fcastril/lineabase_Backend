
using Api.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.Dto;
using Utilidades;
namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi+"[controller]")]
    [ApiController]
    public class ProblemDetailsController : HandlerBaseController<ProblemDetails,ProblemDetailsDto>
    {
        
        public ProblemDetailsController(IMediator mediator,IValidator<ProblemDetailsDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}