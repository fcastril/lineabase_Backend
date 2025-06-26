
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
    public class CurrentToolController : HandlerBaseController<CurrentTool,CurrentToolDto>
    {
        
        public CurrentToolController(IMediator mediator,IValidator<CurrentToolDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}