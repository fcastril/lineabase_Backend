
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
    public class ToolController : HandlerBaseController<Tool,ToolDto>
    {
        
        public ToolController(IMediator mediator,IValidator<ToolDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}