using System.Collections.Generic;
using System.Threading.Tasks;

using StatsLoader.API.Request;
using StatsLoader.Utils;
using StatsLoader.Properties;
using System.Threading;

namespace StatsLoader
{
    internal class Program
    {
        static ApiDataProcessor apiProcessor = new ApiDataProcessor();

        private static Dictionary<AppConfig.ApiPlatform, (BaseRequest request, string apiKey)> requestByPlatform = new()
        {
            { AppConfig.ApiPlatform.Wildberries, (AppConfig.DefaultWildberriesRequestData, AppConfig.WildberriesApiKey) }
        };

        static async Task Main(string[] args)
        {
            if (Settings.Default.IsFirstRun) 
            { 
                Settings.Default.IsFirstRun = false;

                System.Console.WriteLine("First Run");
                await apiProcessor.FetchAndSaveDataAsync(requestByPlatform);
            }

            while (true)
            {                                 
                Thread.Sleep(30 * 60 * 1000);
                System.Console.WriteLine(System.DateTime.Now.ToShortTimeString());
                await apiProcessor.FetchAndSaveDataAsync(requestByPlatform);
            }
        }
    }
}
