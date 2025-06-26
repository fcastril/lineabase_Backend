using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Port
{
    public interface IGithubIntegrations
    {
        Task<GithubAppResponse> GetGithubAppToken(GithubAppRequest githubAppRequest);
        Task<GithubAppResponse> GetGithubAppRefreshToken(GithubAppRequest githubAppRequest);
        Task<List<GithubAppEmailResponse>> GetGithubAppEmail(string token);

    }
}