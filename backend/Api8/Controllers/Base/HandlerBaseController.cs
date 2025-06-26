using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using Util.Common;

namespace Api.Base
{
    /// <summary>  
    /// Base controller for handling CRUD operations and other common actions for entities.  
    /// </summary>  
    /// <typeparam name="ENT">The type of the entity.</typeparam>  
    /// <typeparam name="DTO">The type of the data transfer object.</typeparam> 
#if DEBUG
    [AllowAnonymous]
#else
  [Authorize]
#endif

    public abstract partial class HandlerBaseController<ENT, DTO> : HandlerBaseLiteController<DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        /// <summary>
        /// The validator for the DTO.
        /// </summary>
        protected IValidator<DTO> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerBaseController{ENT, DTO}"/> class.
        /// </summary>
        /// <param name="validator">The validator for the DTO.</param>
        /// <param name="mediator">The mediator for handling requests.</param>
        protected HandlerBaseController(IValidator<DTO> validator, IMediator mediator) : base(mediator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="dto">The data transfer object containing the entity details.</param>
        /// <returns>An IActionResult indicating the result of the create operation.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Create(DTO dto)
        {
            await ResponseMessageValidate(dto);
            return this.HandlerResponse(await _mediator.Send(new CreateAsyncCommand<ENT, DTO>(dto)));
        }

        /// <summary>
        /// Creates a new entities.
        /// </summary>
        /// <param name="listDto">The list data transfer object containing the entity details.</param>
        /// <returns>An IActionResult indicating the result of the create operation.</returns>
        [HttpPost("CreateMany")]
        public virtual async Task<IActionResult> CreateMany(List<DTO> listDto)
        {
            return this.HandlerResponse(await _mediator.Send(new CreateManyAsyncCommand<ENT, DTO>(listDto)));
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="dto">The data transfer object containing the updated entity details.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        [HttpPut]
        public virtual async Task<IActionResult> Update(DTO dto)
        {
            await ResponseMessageValidate(dto);
            return this.HandlerResponse(await _mediator.Send(new UpdateAsyncCommand<ENT, DTO>(dto)));
        }


        /// <summary>
        /// Retrieves a list of entities.
        /// </summary>
        /// <returns>An IActionResult containing the list of entities.</returns>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return this.HandlerResponse(await _mediator.Send(new GetByIdAsyncQuery<ENT, DTO>(id)));
        }

        /// <summary>
        /// Retrieves a list of entities.
        /// </summary>
        /// <returns>An IActionResult containing the list of entities.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return this.HandlerResponse(await _mediator.Send(new ToListAsyncQuery<ENT, DTO>()));
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return this.HandlerResponse(await _mediator.Send(new DeleteAsyncCommand<ENT, DTO>(id)));
        }

        /// <summary>
        /// Paginates the list of entities.
        /// </summary>
        /// <param name="paginado">The pagination details.</param>
        /// <returns>An IActionResult containing the paginated list of entities.</returns>
        [HttpPost("paginator")]
        public async Task<IActionResult> Paginator(Paginate<DTO> paginado)
        {
            return this.HandlerResponse<Paginate<DTO>>(await _mediator.Send(new PaginateAsyncQuery<ENT, DTO>(paginado)));
        }

        /// <summary>
        /// Retrieves an entity by a specified property and value.
        /// </summary>
        /// <param name="property">The property to search by.</param>
        /// <param name="value">The value of the property to search for.</param>
        /// <returns>An IActionResult containing the entity that matches the search criteria.</returns>
        [HttpGet("search/{property}/data/{value}")]
        public async Task<IActionResult> GetBy(string property, string value)
        {
            return this.HandlerResponse(await _mediator.Send(new SearchAsyncQuery<ENT, DTO>(property, value)));
        }

        /// <summary>
        /// Retrieves a list of entities by a specified property and value.
        /// </summary>
        /// <param name="property">The property to search by.</param>
        /// <param name="value">The value of the property to search for.</param>
        /// <returns>An IActionResult containing the list of entities that match the search criteria.</returns>
        [HttpGet("searchList/{property}/data/{value}")]
        public async Task<IActionResult> GetListBy(string property, string value)
        {
            return this.HandlerResponse(await _mediator.Send(new SearchListAsyncQuery<ENT, DTO>(property, value)));
        }

        protected async Task ResponseMessageValidate(DTO dto)
        {
            var validate = await _validator.ValidateAsync(dto);
            string errors = string.Empty;

            if (validate.Errors.Count > 0)
            {
                foreach (var item in validate.Errors)
                {
                    errors += item.ErrorMessage + "\n";
                }

                throw new Util.Ex.DomainException(errors);
            }
        }
    }
}
