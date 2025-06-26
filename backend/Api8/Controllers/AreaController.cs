
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
    public class AreaController : HandlerBaseController<Area,AreaDto>
    {
        
        public AreaController(IMediator mediator,IValidator<AreaDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}