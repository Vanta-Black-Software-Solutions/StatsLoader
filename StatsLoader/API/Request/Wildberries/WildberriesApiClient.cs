using StatsLoader.API.Response.Wildberries.DeserializableStruct;
using StatsLoader.Data;
using StatsLoader.Services.NetInteraction;
using StatsLoader.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader.API.Request.Wildberries
{
    public class WildberriesApiClient : ApiClient
    {
        private DatabaseService databaseService = new DatabaseService();
        public WildberriesApiClient(string apiKey) : base (apiKey, AppConfig.ApiPlatform.Wildberries) { }


        public override async Task<bool> GetReport(BaseRequest request)
        {
            try
            {
                Console.WriteLine("🔹 Отправка запроса к API...");

                string jsonResponse = await GetDataAsync(
                    "https://statistics-api.wildberries.ru/api/v5/supplier/reportDetailByPeriod",
                    request.ToQueryParams());

                Console.WriteLine("✅ Ответ получен, длина: " + jsonResponse.Length);

                var parsedData = JsonParser.ParseResponse<ResponseReportDetailByPeriod>(jsonResponse);

                if (parsedData.Count == 0)
                {
                    Console.WriteLine("⚠️ No data parsed!");
                    return false;
                }

                Console.WriteLine("🔹 Сохранение данных в БД...");
                await databaseService.SaveDataAsync("reportdetailbyperiod", parsedData);
                Console.WriteLine("✅ Данные успешно сохранены!");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                return false;
            }
        }



    }
}
