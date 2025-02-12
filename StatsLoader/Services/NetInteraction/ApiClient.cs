using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatsLoader.Services.NetInteraction
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public ApiClient(string apiKey, AppConfig.ApiPlatform apiPlatform)
        {
            httpClient = new HttpClient();
            this.apiKey = apiKey;

            httpClient.DefaultRequestHeaders.Add("Authorization", apiPlatform switch
            {
                AppConfig.ApiPlatform.Wildberries => $"Bearer {this.apiKey}",
                AppConfig.ApiPlatform.Ozon => $"Api-Key {this.apiKey}",
                _ => throw new ArgumentException("Unknown platform API")
            });
        }

        
    }
}
