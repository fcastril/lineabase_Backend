using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ServiceApplication.Interface;

namespace ServiceApplication.CQRS
{

    public record GetGithubAppRefreshTokenQuery(GithubAppRequest GithubAppRequest) : IRequest<GithubAppResponse>;

    public class GetGithubAppRefreshTokenQueryHandler : IRequestHandler<GetGithubAppRefreshTokenQuery, GithubAppResponse>
    {
        protected readonly IGithubService _implementation;

        public GetGithubAppRefreshTokenQueryHandler(IGithubService implementation)
        {
            _implementation = implementation;
        }

        public async Task<GithubAppResponse> Handle(GetGithubAppRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.GetGithubAppRefreshToken(request.GithubAppRequest);
        }
    }

}