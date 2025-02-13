using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.Services;
using StatsLoader.Services.NetInteraction;
using StatsLoader.Data;
using StatsLoader.API.Request;

namespace StatsLoader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var requestByPlatform = new Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)>
            {
                { AppConfig.ApiPlatform.Wildberries, (AppConfig.DefaultWildberriesRequestData, AppConfig.WbApiKey) }
            };

            var apiProcessor = new ApiDataProcessor(requestByPlatform);

            await apiProcessor.FetchAndSaveDataAsync();
        }
    }
}
