using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Integrations
{
    public class GithubIntegrations : IGithubIntegrations
    {
        private const string URLGITHUB = "https://github.com"; 
        private const string URLGITHUBApi = "https://api.github.com";
        private const  string XGITHUBAPIVERSION = "2022-11-28";
        
        public async Task<GithubAppResponse> GetGithubAppToken(GithubAppRequest githubAppRequest)
        {


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", githubAppRequest.ClientId),
                    new KeyValuePair<string, string>("client_secret", githubAppRequest.ClientSecret),
                    new KeyValuePair<string, string>("code", githubAppRequest.Code),
                    new KeyValuePair<string, string>("redirect_uri", githubAppRequest.RedirectUri)
                });

                var response = await client.PostAsync($@"{URLGITHUB}/login/oauth/access_token", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                var queryParams = System.Web.HttpUtility.ParseQueryString(responseString);
                if (queryParams["error"] != null)
                {
                    throw new HttpRequestException($"Error: {queryParams["error"]}, Description: {queryParams["error_description"]}, URI: {queryParams["error_uri"]}");
                }

                var json = new Dictionary<string, object>
                {
                    { "access_token", queryParams["access_token"].ToString() },
                    { "expires_in", Convert.ToInt32(queryParams["expires_in"]) },
                    { "refresh_token", queryParams["refresh_token"].ToString() },
                    { "refresh_token_expires_in", Convert.ToInt32(queryParams["refresh_token_expires_in"] )},
                    { "scope", queryParams["scope"].ToString() },
                    { "token_type", queryParams["token_type"].ToString() }
                };

                responseString = JsonSerializer.Serialize(json);

                return JsonSerializer.Deserialize<GithubAppResponse>(responseString);
            }

        }

         public async Task<GithubAppResponse> GetGithubAppRefreshToken(GithubAppRequest githubAppRequest)
        {


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", githubAppRequest.ClientId),
                    new KeyValuePair<string, string>("client_secret", githubAppRequest.ClientSecret),
                    new KeyValuePair<string, string>("refresh_token", githubAppRequest.RefreshToken),
                    new KeyValuePair<string, string>("grant_type", "refresh_token")
                });

                var response = await client.PostAsync($@"{URLGITHUB}/login/oauth/access_token", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                var queryParams = System.Web.HttpUtility.ParseQueryString(responseString);
                if (queryParams["error"] != null)
                {
                    throw new HttpRequestException($"Error: {queryParams["error"]}, Description: {queryParams["error_description"]}, URI: {queryParams["error_uri"]}");
                }

                var json = new Dictionary<string, object>
                {
                    { "access_token", queryParams["access_token"].ToString() },
                    { "expires_in", Convert.ToInt32(queryParams["expires_in"]) },
                    { "refresh_token", queryParams["refresh_token"].ToString() },
                    { "refresh_token_expires_in", Convert.ToInt32(queryParams["refresh_token_expires_in"] )},
                    { "scope", queryParams["scope"].ToString() },
                    { "token_type", queryParams["token_type"].ToString() }
                };

                responseString = JsonSerializer.Serialize(json);

                return JsonSerializer.Deserialize<GithubAppResponse>(responseString);
            }

        }

        public async Task<List<GithubAppEmailResponse>> GetGithubAppEmail(string token)
        {
           using (var client = new HttpClient())
            {
                string urlApi = $@"{URLGITHUBApi}/user/public_emails";

                var request = new HttpRequestMessage(HttpMethod.Get, urlApi);
                request.Headers.Add("Authorization",$"bearer {token}");
                request.Headers.Add("User-Agent","Swo-Devex-GithubApp");
                // request.Headers.Add("Accept","application/vnd.github+json");
                // request.Headers.Add("X-Github-Api.Version", XGITHUBAPIVERSION);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<GithubAppEmailResponse>>(responseString);
            }
        }
    }
}