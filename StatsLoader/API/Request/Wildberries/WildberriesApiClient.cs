using StatsLoader.API.Response.Wildberries.DeserializableStruct;
using StatsLoader.Data;
using StatsLoader.Services.NetInteraction;
using StatsLoader.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatsLoader.API.Request.Wildberries
{
    public class WildberriesApiClient : ApiClient
    {
        private DatabaseService databaseService = new DatabaseService();

        public WildberriesApiClient(string apiKey) : base(apiKey, AppConfig.ApiPlatform.Wildberries) { }

        public override async Task<bool> GetReport(BaseRequest request)
        {
            try
            {
                Console.WriteLine("Request To API...");

                string jsonResponse = await GetDataAsync(
                    "https://statistics-api.wildberries.ru/api/v5/supplier/reportDetailByPeriod",
                    request.ToQueryParams());

                Console.WriteLine($"Response Size: {jsonResponse.Length}bytes");

                var parsedData = JsonParser.ParseResponse<ResponseReportDetailByPeriod>(jsonResponse);

                if (parsedData.Count == 0)
                {
                    Console.WriteLine("No data parsed!");
                    return false;
                }

                Console.WriteLine("Save in DB...");
                await databaseService.SaveDataAsync("reportdetailbyperiod", parsedData);
                Console.WriteLine("Saved");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
