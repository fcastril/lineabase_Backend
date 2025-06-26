using MediatR;
using Microsoft.AspNetCore.Mvc;
using Util.Common;

namespace Api.Base
{

    /// <summary>
    /// Base controller for handling requests with a DTO.
    /// </summary>
    /// <typeparam name="DTO">The type of the data transfer object.</typeparam>
    public class HandlerBaseLiteController<DTO> : Controller
    {

        /// <summary>
        /// The mediator instance.
        /// </summary>
        protected IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerBaseLiteController{DTO}"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        public HandlerBaseLiteController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Handles the response for a given DTO.
        /// </summary>
        /// <param name="dato">The data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        public IActionResult HandlerResponse<DTO>(DTO dato)
        {
            return this.Ok(new ResponseApi<DTO> { Data = dato, Status = true, Message = "Operation carried out successfully." });
        }

        /// <summary>
        /// Handles the response for a given DTO.
        /// </summary>
        /// <param name="dato">The data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        public IActionResult HandlerResponseNotFound<DTO>(DTO dato)
        {
            return this.NotFound(new ResponseApi<DTO> { Data = dato, Status = false, Message = "Operation carried out with errors." });
        }
    }
}
