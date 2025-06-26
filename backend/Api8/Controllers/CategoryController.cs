
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
    public class CategoryController : HandlerBaseController<Category,CategoryDto>
    {
        
        public CategoryController(IMediator mediator,IValidator<CategoryDto> validator): base(validator,mediator)
        {
            
            
        }
        
    }
}