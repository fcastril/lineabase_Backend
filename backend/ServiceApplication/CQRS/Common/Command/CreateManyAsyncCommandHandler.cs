using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record CreateManyAsyncCommand<ENT, DTO>(List<DTO> Dto) : IRequest<bool>
        where ENT : class, new()
        where DTO : class, new();

    public class CreateManyAsyncCommandHandler<ENT, DTO> : IRequestHandler<CreateManyAsyncCommand<ENT, DTO>, bool>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public CreateManyAsyncCommandHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(CreateManyAsyncCommand<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.CreateModels(request.Dto);
        }
    }
}

