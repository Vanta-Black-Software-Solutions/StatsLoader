using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatsLoader.API.Request;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.Data;
using StatsLoader.Factories;
using StatsLoader.Services.NetInteraction;

namespace StatsLoader.Utils
{
    internal class ApiDataProcessor
    {
        /// <summary>
        /// Запрашивает и сохраняет данные для каждой платформы.
        /// </summary>
        /// <returns>Задача без возвращаемого значения.</returns>
        public async Task FetchAndSaveDataAsync(Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform)
        {
            IApiClient apiClient;

            try
            {
                foreach (var entry in requestByPlatform)
                {
                    var platform = entry.Key;
                    var request = entry.Value.request;
                    var apiKey = entry.Value.apiKey;

                    apiClient = ApiClientFactory.Create(platform, apiKey);

                    if (await apiClient.GetReport(request))
                        Console.WriteLine("OK!", Console.ForegroundColor = ConsoleColor.Green);
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
