using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Interface;

namespace ServiceApplication.Models.Github.Service
{

    public class GithubService : IGithubService
    {
        private readonly IGithubIntegrations _githubIntegrations;
        public GithubService(IGithubIntegrations githubIntegrations)
        {
            _githubIntegrations = githubIntegrations;

        }
        public async Task<GithubAppResponse> GetGithubAppToken(GithubAppRequest githubAppRequest)
        {

            return await _githubIntegrations.GetGithubAppToken(githubAppRequest);
        }
        public async Task<GithubAppResponse> GetGithubAppRefreshToken(GithubAppRequest githubAppRequest)
        {

            return await _githubIntegrations.GetGithubAppRefreshToken(githubAppRequest);
        }

        public async Task<List<GithubAppEmailResponse>> GetGithubAppEmail(string token)
        {
            return await _githubIntegrations.GetGithubAppEmail(token);
        }
    }
}