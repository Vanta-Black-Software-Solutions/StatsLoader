using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatsLoader.API.Request;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.Data;
using StatsLoader.Services.NetInteraction;

namespace StatsLoader.Services
{
    internal class ApiDataProcessor
    {

        Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform;
        public ApiDataProcessor (Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform)
        {
            this.requestByPlatform = requestByPlatform;
        }

        public async Task FetchAndSaveDataAsync()
        {
            ApiClient apiClient;

            try
            {
                foreach (var request in requestByPlatform)
                {
                    apiClient = request.Key switch
                    {
                        AppConfig.ApiPlatform.Wildberries => new WildberriesApiClient(request.Value.apiKey),
                        //AppConfig.ApiPlatform.Ozon => new WildberriesApiClient(request.Value.apiKey),
                        //AppConfig.ApiPlatform.Yandex => new WildberriesApiClient(request.Value.apiKey)
                        _ => throw new ArgumentException("Unknown Platform")
                    };
                    if (await apiClient.GetReport(request.Value.request))
                    {
                        Console.WriteLine("OK!");
                    }
                    else
                        throw new ArgumentException("NO!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling Error: {ex.Message}");
            }
        }




    }
}
