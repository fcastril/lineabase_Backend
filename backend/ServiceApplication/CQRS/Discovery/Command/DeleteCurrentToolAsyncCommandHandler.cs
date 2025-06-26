using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{ 

    public record DeleteCurrentToolAsyncCommand(string id) : IRequest<bool>;
    public class DeleteCurrentToolAsyncCommandHandler : IRequestHandler<DeleteCurrentToolAsyncCommand, bool>
    {
        private readonly IBaseServiceApplication<CurrentTool, CurrentToolDto> _implementation;

        public DeleteCurrentToolAsyncCommandHandler(
            IBaseServiceApplication<CurrentTool, CurrentToolDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteCurrentToolAsyncCommand request, CancellationToken cancellationToken)
        {


            return await _implementation.DeleteModel(request.id);
        }
    }
}
