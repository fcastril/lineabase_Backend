using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace ServiceApplication.Interface
{
    public interface IGithubService
    {
        Task<GithubAppResponse> GetGithubAppToken(GithubAppRequest githubAppRequestDto);
        Task<GithubAppResponse> GetGithubAppRefreshToken(GithubAppRequest githubAppRequestDto);
        Task<List<GithubAppEmailResponse>> GetGithubAppEmail(string token);
    }
}