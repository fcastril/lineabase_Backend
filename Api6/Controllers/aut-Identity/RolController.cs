﻿using Api.Base;
using Api6.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication;
using ServiceApplication.Dto;
using Utilidades;

namespace Api.Controllers
{
    [Route(ConstantsAPI.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class RolController : HandlerBaseController<Rol, RolDto>
    {
        public RolController(IValidator<RolDto> validator, IMediator mediator)
            : base(validator, mediator)
        {

        }

    }
}
