using StatsLoader.Services.NetInteraction;
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
        public WildberriesApiClient(string apiKey) : base (apiKey, AppConfig.ApiPlatform.Wildberries)
        {

        }

        public async Task<string> GetReportDetailByPeriod(Dictionary<string, string> queryParams)
        {
            return await GetDataAsync("https://statistics-api.wildberries.ru/api/v5/supplier/reportDetailByPeriod", queryParams);
        }  
    }
}
