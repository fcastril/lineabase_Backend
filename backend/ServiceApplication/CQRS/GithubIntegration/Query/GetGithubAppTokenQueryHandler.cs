using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ServiceApplication.Interface;

namespace ServiceApplication.CQRS
{

    public record GetGithubAppTokenQuery(GithubAppRequest GithubAppRequest) : IRequest<GithubAppResponse>;

    public class GetGithubAppTokenQueryHandler : IRequestHandler<GetGithubAppTokenQuery, GithubAppResponse>
    {
        protected readonly IGithubService _implementation;

        public GetGithubAppTokenQueryHandler(IGithubService implementation)
        {
            _implementation = implementation;
        }

        public async Task<GithubAppResponse> Handle(GetGithubAppTokenQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.GetGithubAppToken(request.GithubAppRequest);
        }
    }

}