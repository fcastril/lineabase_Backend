using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ServiceApplication.Interface;

namespace ServiceApplication.CQRS
{
    public record GetGithubAppEmailsQuery(string token) : IRequest<List<GithubAppEmailResponse>>;

    public class GetGithubAppEmailsQueryHandler : IRequestHandler<GetGithubAppEmailsQuery, List<GithubAppEmailResponse>>
    {
        protected readonly IGithubService _implementation;

        public GetGithubAppEmailsQueryHandler(IGithubService implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<GithubAppEmailResponse>> Handle(GetGithubAppEmailsQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.GetGithubAppEmail(request.token);
        }
    }
}