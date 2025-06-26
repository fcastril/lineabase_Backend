
using Api.Base;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Port;
using System.Text;
using Util.Common;
using Utilidades;
namespace Api.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class DiscoveryController : HandlerBaseController<Discovery, DiscoveryDto>
    {
        private readonly IValidator<CurrentToolDto> _currentToolValidator;
        private readonly IValidator<ProblemDetailsDto> _problemDetailsValidator;
        private readonly IMessageSender<MessageQueue> _messageSender;
        private readonly IConfiguration _configuration;

        public DiscoveryController(IMediator mediator,
            IValidator<DiscoveryDto> validator,
            IValidator<CurrentToolDto> currentToolValidator,
            IValidator<ProblemDetailsDto> problemDetailsValidator,
            IMessageSender<MessageQueue> messageSender,
            IConfiguration configuration) : base(validator, mediator)
        {
            _currentToolValidator = currentToolValidator;
            _problemDetailsValidator = problemDetailsValidator;
            _messageSender = messageSender;
            _configuration = configuration;
        }

        [HttpPost("SendMessageToQueue")]
        public async Task<IActionResult> SendMessageToQueue([FromBody] MessageQueue messageQueue)
        {
            messageQueue.Tool = Tools.GenAIBenefits.ToString();
            await _messageSender.SendAsync(messageQueue);

            return Ok();
        }

        [HttpPost("CurrentTool/{discoveryId}")]
        public async Task<IActionResult> CreateCurrentTool(string discoveryId, [FromBody] CurrentToolDto currentToolDto) => await CommandOperations<CurrentToolDto, CreateCurrentToolAsyncCommand>(discoveryId, currentToolDto, new CreateCurrentToolAsyncCommand(currentToolDto), _currentToolValidator);

        [HttpPut("CurrentTool/{discoveryId}")]
        public async Task<IActionResult> UpdateCurrentTool(string discoveryId, [FromBody] CurrentToolDto currentToolDto) => await CommandOperations<CurrentToolDto, UpdateCurrentToolAsyncCommand>(discoveryId, currentToolDto, new UpdateCurrentToolAsyncCommand(currentToolDto), _currentToolValidator);

        [HttpGet("CurrentTool/{discoveryId}")]
        public async Task<IActionResult> GetCurrentTool(string discoveryId) => this.HandlerResponse(await _mediator.Send(new ToListCurrentToolAsyncQuery(discoveryId)));

        [HttpGet("CurrentTool/GetById/{id}")]
        public async Task<IActionResult> GetByIdCurrentTool(string id) => this.HandlerResponse(await _mediator.Send(new GetByIdCurrentToolAsyncQuery(id)));

        [HttpDelete("CurrentTool/{id}")]
        public async Task<IActionResult> DeleteCurrentTool(string id) => this.HandlerResponse(await _mediator.Send(new DeleteCurrentToolAsyncCommand(id)));

        [HttpPost("CurrentTool/paginator/{discoveryId}")]
        public async Task<IActionResult> PaginatorCurrentTool(string discoveryId, [FromBody] Paginate<CurrentToolDto> paginado) => this.HandlerResponse(await _mediator.Send(new PaginateCurrentToolAsyncQuery(paginado, discoveryId)));

        [HttpPost("ProblemDetails/{discoveryId}")]
        public async Task<IActionResult> CreateProblemDetails(string discoveryId, [FromBody] ProblemDetailsDto problemDetailsDto) => await CommandOperations<ProblemDetailsDto, CreateProblemDetailsAsyncCommand>(discoveryId, problemDetailsDto, new CreateProblemDetailsAsyncCommand(problemDetailsDto), _problemDetailsValidator);

        [HttpPut("ProblemDetails/{discoveryId}")]
        public async Task<IActionResult> UpdateProblemDetails(string discoveryId, [FromBody] ProblemDetailsDto problemDetailsDto) => await CommandOperations<ProblemDetailsDto, UpdateProblemDetailsAsyncCommand>(discoveryId, problemDetailsDto, new UpdateProblemDetailsAsyncCommand(problemDetailsDto), _problemDetailsValidator);

        [HttpGet("ProblemDetails/{discoveryId}")]
        public async Task<IActionResult> GetProblemDetails(string discoveryId) => this.HandlerResponse(await _mediator.Send(new ToListProblemDetailsAsyncQuery(discoveryId)));

        [HttpGet("ProblemDetails/GetById/{id}")]
        public async Task<IActionResult> GetByIdProblemsDetail(string id) => this.HandlerResponse(await _mediator.Send(new GetByIdProblemDetailsAsyncQuery(id)));

        [HttpDelete("ProblemDetails/{id}")]
        public async Task<IActionResult> DeleteProblemDetails(string id) => this.HandlerResponse(await _mediator.Send(new DeleteProblemDetailsAsyncCommand(id)));

        [HttpPost("ProblemDetails/paginator/{discoveryId}")]
        public async Task<IActionResult> PaginatorProblemDetails(string discoveryId, [FromBody] Paginate<ProblemDetailsDto> paginado) => this.HandlerResponse(await _mediator.Send(new PaginateProblemDetailsAsyncQuery(paginado, discoveryId)));

        [HttpGet("Customer/GetById/{id}")]
        public async Task<IActionResult> GetByIdCustomerId(string id) => this.HandlerResponse(await _mediator.Send(new GetByIdCustomerAsyncQuery(id)));

        private static async Task<string> Validation<T>(T dto, IValidator<T> validator)
            where T : class
        {
            var validate = await validator.ValidateAsync(dto);
            string message = string.Empty;
            if (validate.Errors.Count > 0)
            {
                StringBuilder messageBuilder = new StringBuilder();
                foreach (var error in validate.Errors)
                {
                    if (messageBuilder.Length > 0)
                    {
                        messageBuilder.Append(", ");
                    }
                    messageBuilder.Append(error.ErrorMessage);
                }
                message = messageBuilder.ToString();
            }
            return message;
        }

        private async Task<IActionResult> CommandOperations<DTO, T>(string discoveryId, DTO dto, T command, IValidator<DTO> validator)
            where DTO : class
        {
            var property = dto.GetType().GetProperty("DiscoveryId") ?? throw new ArgumentException($"The property 'DiscoveryId' does not exist on type '{typeof(DTO).Name}'.");
            property.SetValue(dto, discoveryId);

            var response = await Validation(dto, validator);
            if (response != string.Empty)
            {
                return this.HandlerResponseNotFound(response);
            }

            return this.HandlerResponse(await _mediator.Send(command));
        }
    }
}