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

namespace StatsLoader.Utils
{
    internal class ApiDataProcessor
    {

        private Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform;

        public ApiDataProcessor (Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform)
        {
            this.requestByPlatform = requestByPlatform;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                        Console.WriteLine("OK!", Console.ForegroundColor = ConsoleColor.Green);
                    }
                    else
                        throw new ArgumentException("NO!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling Error: {ex.Message}", Console.ForegroundColor = ConsoleColor.Red);
            }
        }




    }
}
