using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatsLoader.Data;
using StatsLoader.Services.NetInteraction;

namespace StatsLoader.Services
{
    internal class ApiDataProcessor
    {
        private readonly IApiClient _apiClient;
        private readonly IDatabaseService _databaseService;

        public ApiDataProcessor(IApiClient apiClient, IDatabaseService databaseService)
        {
            _apiClient = apiClient;
            _databaseService = databaseService;
        }

        public async Task FetchAndSaveDataAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null)
            where T : class
        {
            try
            {
                string jsonData = await _apiClient.GetDataAsync(endpoint, queryParams);
                var data = JsonSerializer.Deserialize<List<T>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data != null && data.Count > 0)
                {
                    await _databaseService.SaveDataAsync(data);
                    Console.WriteLine($"Saved {typeof(T).Name} ");
                }
                else
                {
                    Console.WriteLine($"Data No Found{typeof(T).Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling Error {typeof(T).Name}: {ex.Message}");
            }
        }
    }
}
