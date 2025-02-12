using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.Services;
using StatsLoader.Services.NetInteraction;
using StatsLoader.API.Response.Wildberries;
using StatsLoader.Data;
using StatsLoader.Utils;

namespace StatsLoader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Создаём API клиент и сервис работы с БД
            IApiClient apiClient = new ApiClient(AppConfig.WbApiKey, AppConfig.ApiPlatform.Wildberries);
            IDatabaseService databaseService = new DatabaseService();
            var processor = new ApiDataProcessor(apiClient, databaseService);

            // Заголовки для запроса
            var headers = new Dictionary<string, string>
            {
                { "dateFrom", "2024-02-01T00:00:00Z" },
                { "dateTo", "2024-02-10T00:00:00Z" },
                { "limit", "100" }
            };

            // Проходим по всем эндпоинтам
            foreach (var endpoint in WbApiEndpoints.ApiEndpoints)
            {
                string endpointName = endpoint.Key;
                string url = endpoint.Value.FullUrl;
                bool isPost = endpoint.Value.IsPost;

                Console.WriteLine($"🚀 Запрашиваем данные: {endpointName}");

                try
                {
                    if (isPost)
                    {
                        // Делаем POST-запрос
                        var body = new { dateFrom = "2024-02-01T00:00:00Z", dateTo = "2024-02-10T00:00:00Z", limit = 100 };
                        string jsonResponse = await apiClient.PostDataAsync(url, body, headers);

                        var data = JsonParser.ParseResponse<reportDetailByPeriod>(jsonResponse);
                        await databaseService.SaveDataAsync(data);
                    }
                    else
                    {
                        // Делаем GET-запрос
                        await processor.FetchAndSaveDataAsync<reportDetailByPeriod>(url, headers);
                    }
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Ошибка при обработке {endpointName}: {ex.Message}");
                }
                
            }

            Console.WriteLine("✅ Все данные загружены и сохранены!");
        }
    }
}
