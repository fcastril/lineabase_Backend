using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{

    public record GetByIdProblemDetailsAsyncQuery(string id) : IRequest<ProblemDetailsDto>;

    public class GetByIdProblemDetailsAsyncQueryHandler : IRequestHandler<GetByIdProblemDetailsAsyncQuery, ProblemDetailsDto>
    {
        protected readonly IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> _implementation;

        public GetByIdProblemDetailsAsyncQueryHandler(IBaseServiceApplication<ProblemDetails, ProblemDetailsDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<ProblemDetailsDto> Handle(GetByIdProblemDetailsAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.FirstOrDefautlModelBy(x => x.Id == request.id);
        }
    }
}

