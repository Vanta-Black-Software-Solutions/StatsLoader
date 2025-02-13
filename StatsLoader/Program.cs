using System.Collections.Generic;
using System.Threading.Tasks;
using StatsLoader.API.Request;
using StatsLoader.Utils;

namespace StatsLoader
{
    internal class Program
    {

        private static Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform = new()
        {
            { AppConfig.ApiPlatform.Wildberries, (AppConfig.DefaultWildberriesRequestData, AppConfig.WbApiKey) }
        };

        static async Task Main(string[] args)
        {
            ApiDataProcessor apiProcessor = new ApiDataProcessor(requestByPlatform);
            await apiProcessor.FetchAndSaveDataAsync();
        }
    }
}
