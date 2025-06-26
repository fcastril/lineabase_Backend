using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceApplication.Functions
{
    public class GenAI : IGenAI
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GenAI(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SendAsync(string discoveryId, string name)
        {
            Dictionary<string, string> queryParams = new()
            {
                { "discoveryId", discoveryId },
                { "name", name }
            };

            return await Send(queryParams) == "true";
        }

        private async Task<string> Send(Dictionary<string, string> queries)
        {
            string url = _configuration.GetSection("Functions:GenAI:UrlBase").Value;
            string endpoint = _configuration.GetSection("Functions:GenAI:OrchMethod").Value;
            string requestUrl = $"{url}{endpoint}";

            Dictionary<string, string> queryParams = queries;

            var uriBuilder = new UriBuilder(requestUrl);
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in queryParams)
            {
                query[param.Key] = param.Value;
            }
            uriBuilder.Query = query.ToString();

            HttpRequestMessage request = new(HttpMethod.Get, uriBuilder.Uri);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
