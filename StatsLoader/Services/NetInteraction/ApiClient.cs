using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatsLoader.API.Request;

namespace StatsLoader.Services.NetInteraction
{
    public abstract class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public abstract Task<bool> GetReport(BaseRequest request);

        public async Task<string> GetDataAsync(string endpoint, Dictionary<string, string>? queryParams = null)
        {
            try
            {
                if (queryParams != null)
                {
                    string requestURL = string.Join("&", queryParams.Select(p => $"{p.Key}={p.Value}"));
                    endpoint += $"?{requestURL}";
                }

                var response = await httpClient.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return "{}";
            }
        }

        public async Task<string> PostDataAsync(string endpoint, object body)
        {
            try
            {
                string jsonBody = JsonSerializer.Serialize(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return "{}";
            }
        }

        public ApiClient(string apiKey, AppConfig.ApiPlatform apiPlatform)
        {
            httpClient = new HttpClient();
            this.apiKey = apiKey;

            httpClient.DefaultRequestHeaders.Add("Authorization", apiPlatform switch
            {
                AppConfig.ApiPlatform.Wildberries => $"Bearer {this.apiKey}",
                AppConfig.ApiPlatform.Ozon => $"Api-Key {this.apiKey}",
                AppConfig.ApiPlatform.Yandex => $"{this.apiKey}", // TO DO: подставить апи яндекса 
                _ => throw new ArgumentException("Unknown platform API")
            });
        }
    }
}
