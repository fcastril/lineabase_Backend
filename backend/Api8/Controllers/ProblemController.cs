
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
    public class ProblemController : HandlerBaseController<Problem,ProblemDto>
    {
        
        public ProblemController(IMediator mediator,IValidator<ProblemDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}