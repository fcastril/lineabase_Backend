
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
    public class SectorController : HandlerBaseController<Sector,SectorDto>
    {
        
        public SectorController(IMediator mediator,IValidator<SectorDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}