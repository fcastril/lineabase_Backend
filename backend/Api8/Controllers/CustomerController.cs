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
    public class CustomerController : HandlerBaseController<Customer,CustomerDto>
    {
        public CustomerController(IMediator mediator,IValidator<CustomerDto> validator): base(validator,mediator)
        {
            
        }
    }
}