
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
    public class FrecuencyController : HandlerBaseController<Frecuency,FrecuencyDto>
    {
        
        public FrecuencyController(IMediator mediator,IValidator<FrecuencyDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}