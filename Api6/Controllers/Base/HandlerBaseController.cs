﻿using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using Util.Common;

namespace Api.Base
{
#if DEBUG
    [AllowAnonymous]
    #else
        [Authorize]
    #endif
    public abstract partial class HandlerBaseController<ENT, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IValidator<DTO> _validator;

        public HandlerBaseController(IValidator<DTO> validator, IMediator mediator): base(mediator)
        {
            _validator = validator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(DTO dto)
        {
            var validate = await _validator.ValidateAsync(dto);
            if (validate.Errors.Count > 0)
            {
                throw new Util.Ex.DomainException( JsonSerializer.Serialize( validate.Errors));
            }
            return this.HandlerResponse(await _mediator.Send(new CreateAsyncCommand<ENT, DTO>(dto)));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(DTO dto)
        {
            var validate = await _validator.ValidateAsync(dto);
            if (validate.Errors.Count > 0)
            {
                throw new Util.Ex.DomainException(JsonSerializer.Serialize(validate.Errors));
            }
            return this.HandlerResponse(await _mediator.Send(new UpdateAsyncCommand<ENT, DTO>(dto)));
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return this.HandlerResponse(await _mediator.Send(new ToListAsyncQuery<ENT, DTO>()));
        }
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return this.HandlerResponse(await _mediator.Send(new GetByIdAsyncQuery<ENT,DTO>(id)));;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return this.HandlerResponse(await _mediator.Send(new DeleteAsyncCommand<ENT, DTO>(id)));
        }

        [HttpPost("paginator")]
        public async Task<IActionResult> Paginator(Paginate<DTO> paginado)
        {
            return this.HandlerResponse<Paginate<DTO>>(await _mediator.Send(new PaginateAsyncQuery<ENT, DTO>(paginado)));
        }
        [HttpGet("search/{property}/data/{value}")]
        public async Task<IActionResult> GetBy(string property, string value)
        {
            return this.HandlerResponse(await _mediator.Send(new SearchAsyncQuery<ENT, DTO>(property, value)));
        }
        [HttpGet("searchList/{property}/data/{value}")]
        public async Task<IActionResult> GetListBy(string property, string value)
        {
            return this.HandlerResponse(await _mediator.Send(new SearchListAsyncQuery<ENT, DTO>(property, value)));
        }



    }
}
