using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record DeleteProblemDetailsAsyncCommand(string id) : IRequest<bool>;
    public class DeleteProblemDetailsAsyncCommandHandler : IRequestHandler<DeleteProblemDetailsAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> _implementation;

        public DeleteProblemDetailsAsyncCommandHandler(
            IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteProblemDetailsAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteModel(request.id);
        }
    }
}
